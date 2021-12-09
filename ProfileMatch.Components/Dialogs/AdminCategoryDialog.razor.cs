﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Dialogs
{
    public partial class AdminCategoryDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Category Cat { get; set; } = new();
        public string TempName { get; set; }
        public string TempDescription { get; set; }

        protected override void OnInitialized()
        {
            TempName = Cat.Name;
            TempDescription = Cat.Description;
        }
        private static IEnumerable<string> MaxCharacters(string ch)
        {
            if (!string.IsNullOrEmpty(ch) && 21 < ch?.Length)
                yield return "Max 20 characters";
        }
        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }

        private MudForm Form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add("Operation cancelled", Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                Cat.Name = TempName;
                Cat.Description = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"There was an error: {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(Cat));
            }
        }

        private async Task Save()
        {
            if (Cat.Id == 0)
            {
                var result = await CategoryRepository.Insert(Cat);
                Snackbar.Add($"Category {result.Name} created", Severity.Success);
            }
            else
            {
                var result = await CategoryRepository.Update(Cat);
                Snackbar.Add($"Category {result.Name} updated", Severity.Success);
            }
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}