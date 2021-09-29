﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;



using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.Responses;


namespace ProfileMatch.Sites.Admin
{
    public partial class EditUser : ComponentBase
    {
        [Inject]
        AuthenticationStateProvider authSP { get; set; }

        [Inject]
        UserManager<ApplicationUser> UserManager { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Parameter] public string Id { get; set; }
        ApplicationUser currentUser = new();
        protected MudForm Form { get; set; } // TODO add validations
        private bool _success;
        DateTime? _dob;
        string currentUserName;
        ApplicationUser User { get; set; } = new();
        private List<Department> Departments = new();
        private async Task GetUserDetails()
        {
var authState = await authSP.GetAuthenticationStateAsync();
        var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
        currentUser = await UserManager.GetUserAsync(user);
        currentUserName = currentUser.FirstName + " " + currentUser.LastName;
            }
            else
            {
                currentUserName = "Please log in.";
            }

        }
        
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
      
            await GetUserDetails();
        }

        private async Task LoadData()
        {
            Departments = await DepartmentService.FindAllAsync();
            User = await UserService.FindSingleByIdAsync(Id);
            StateHasChanged();
        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                User.UserName = User.Email;
                User.NormalizedEmail = User.Email.ToUpper();
                if (await UserService.Exist(User.Email))
                {
                   
                await UserService.Update(User);
                }
                else
                {
                   await UserService.Create(User);
                }

                NavigationManager.NavigateTo("/admin/dashboard");
            }
        }
    }
}
