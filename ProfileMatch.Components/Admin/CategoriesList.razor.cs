﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Admin
{
    public partial class CategoriesList : ComponentBase
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        public ICategoryRepository CategoryRepository { get; set; }

        private async Task<List<Category>> GetCategoriesAsync()
        {
            return await CategoryRepository.GetCategoriesWithQuestions();
        }

        protected override async Task OnInitializedAsync()
        {
            Categories = await GetCategoriesAsync();
        }

        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;
        private string searchString1 = "";
        private Category selectedItem1 = null;
        private List<Category> Categories = new();

        private bool FilterFunc1(Category category) => FilterFunc(category, searchString1);

        private static bool FilterFunc(Category category, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (category.Description != null && category.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private async Task CategoryUpdate(Category category)
        {
            var parameters = new DialogParameters { ["Cat"] = category };
            var dialog = DialogService.Show<AdminEditCategoryDialog>("Update Category", parameters);
            await dialog.Result;
        }

        private async Task CategoryCreate()
        {
            var dialog = DialogService.Show<AdminEditCategoryDialog>("Create Category");
            await dialog.Result;
            Categories = await GetCategoriesAsync();
        }

        private async Task QuestionCreate(Category category)
        {
            var parameters = new DialogParameters { ["CategoryId"] = category.Id };
            var dialog = DialogService.Show<AdminEditQuestionDialog>($"Create Question for {category.Name}", parameters);
             await dialog.Result;
            Categories = await GetCategoriesAsync();
        }
    }
}