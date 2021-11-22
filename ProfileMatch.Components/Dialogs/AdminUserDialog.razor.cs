﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminUserDialog : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject] UserManager<ApplicationUser> UserManager { get; set; }
        [Inject] RoleManager<IdentityRole> RoleManager { get; set; }
  
        List<UserRolesVM> UserRoles = new();
        [Inject] DataManager<Department, ApplicationDbContext> DepartmentRepository { get; set; }

        [Parameter] public string Id { get; set; }
        protected MudForm Form { get; set; } // TODO add validations
        private DateTime? _dob;
        [Parameter] public ApplicationUser EditedUser { get; set; } = new();
        private List<Department> Departments = new();
        protected override async Task OnInitializedAsync()
        {
            await LoadData();

        }

        private async Task LoadData()
        {
            foreach (var role in RoleManager.Roles)
            {
                var userRolesVM = new UserRolesVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    UserId = EditedUser.Id
                };
                if (await UserManager.IsInRoleAsync(EditedUser, role.Name))
                {
                    userRolesVM.IsSelected = true;
                }
                else
                {
                    userRolesVM.IsSelected = false;
                }
                UserRoles.Add(userRolesVM);
            }
            Departments = await DepartmentRepository.Get();

            if (EditedUser.DateOfBirth == null)
            {
                _dob = DateTime.Now;
            }
            else
            {
                _dob = EditedUser.DateOfBirth;
            }
        }
        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                EditedUser.DateOfBirth = (DateTime)_dob;
                EditedUser.UserName = EditedUser.Email;
                EditedUser.NormalizedEmail = EditedUser.Email.ToUpper();
                var exists = await UserManager.FindByEmailAsync(EditedUser.Email);
                if (exists is not null)
                {
                    // Update the user
                    await UserManager.UpdateAsync(EditedUser);
                }
                else
                {
                    await UserManager.CreateAsync(EditedUser, EditedUser.PasswordHash);
                }
                foreach (var role in UserRoles)
                {
                    if (role.IsSelected&&!await UserManager.IsInRoleAsync(EditedUser, role.RoleName))
                    {
                        await UserManager.AddToRoleAsync(EditedUser, role.RoleName);
                    }
                    if (!role.IsSelected && await UserManager.IsInRoleAsync(EditedUser, role.RoleName))
                    {
                        await UserManager.RemoveFromRoleAsync(EditedUser,role.RoleName);
                    }
                }

                StateHasChanged();
                NavigationManager.NavigateTo("/admin/dashboard");
            }
        }

        private IBrowserFile file;

        private async void UploadFile(InputFileChangeEventArgs e)
        {
            file = e.File;
            var buffers = new byte[file.Size];
            await file.OpenReadStream(maxFileSize).ReadAsync(buffers);
            EditedUser.PhotoPath = $"data:{file.ContentType};base64,{Convert.ToBase64String(buffers)}";
            StateHasChanged();
        }

        private readonly long maxFileSize = 1024 * 1024 * 15;

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}