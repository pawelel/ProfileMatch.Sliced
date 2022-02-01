﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Entities;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin.Dialogs
{
    public partial class AdminCategoryDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        [Parameter] public Category Cat { get; set; } = new();
        public string TempName { get; set; }
        public string TempNamePl { get; set; }
        public string TempDescription { get; set; }
        public string TempDescriptionPl { get; set; }
        bool _isOpen = false;
        bool _deleteEnabled;
        public void ToggleOpen()
        {
            _isOpen = !_isOpen;
        }
        protected override async Task OnInitializedAsync()
        {
            _deleteEnabled = await UnitOfWork.Categories.ExistById(Cat.Id);
            TempName = Cat.Name;
            TempNamePl = Cat.NamePl;
            TempDescription = Cat.Description;
            TempDescriptionPl = Cat.DescriptionPl;
        }
        private IEnumerable<string> MaxCharacters(string ch)
        {
            if (!string.IsNullOrEmpty(ch) && 21 < ch?.Length)
                yield return L["Max 20 characters"];
        }

        private MudForm _form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(L["Operation cancelled"], Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                Cat.Name = TempName;
                Cat.Description = TempDescription;
                Cat.NamePl = TempNamePl;
                Cat.DescriptionPl = TempDescriptionPl;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error:"] + $" {@L[ex.Message]}", Severity.Error);
                }
                MudDialog.Close(DialogResult.Ok(Cat));
            }
        }
        private async Task Delete()
        {
            if (_deleteEnabled)
            {
               
                await UnitOfWork.Categories.Delete(Cat);
            }
            if (ShareResource.IsEn())
            {
                Snackbar.Add($"Category {Cat.Name} deleted");
            }
            else
            {
                Snackbar.Add($"Kategoria {Cat.NamePl} usunięta");
            }
        }
        private async Task Save()
        {
            string created;
            string updated;
            if (ShareResource.IsEn())
            {
                created = $"Category {Cat.Name} created";
                updated = $"Category {Cat.Name} updated";
            }
            else
            {
                created = $"Kategoria {Cat.NamePl} utworzona";
                updated = $"Kategoria {Cat.NamePl} zaktualizowana";
            }
            if (Cat.Id == 0)
            {
                var result = await UnitOfWork.Categories.Insert(Cat);
                Snackbar.Add(created, Severity.Success);
            }
            else
            {
                var result = await UnitOfWork.Categories.Update(Cat);
                Snackbar.Add(updated, Severity.Success);
            }
        }
    }
}