﻿using System.Threading.Tasks;

using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

using ProfileMatch.Contracts;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.Dialogs
{
    public partial class EditQuestionDialog : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Parameter] public Question Q { get; set; } = new();
        [Parameter] public int CategoryId { get; set; }
        public string TempName { get; set; }
        public string TempDescription { get; set; }

        protected override void OnInitialized()
        {
            TempName = Q.Name;
            TempDescription = Q.Description;
        }

        [Inject] public IQuestionRepository QuestionRepository { get; set; }

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
                Q.Name = TempName;
                Q.Description = TempDescription;
                Q.CategoryId = CategoryId;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"There was an error: {ex.Message}", Severity.Error);
                }

                MudDialog.Close(DialogResult.Ok(Q));
            }
        }

        private async Task Save()
        {
            if (Q.Id == 0)
            {
                var result = await QuestionRepository.Create(Q);
                Snackbar.Add($"Question {result.Name} created", Severity.Success);
            }
            else
            {
                var result = await QuestionRepository.Update(Q);
                Snackbar.Add($"Question {result.Name} updated", Severity.Success);
            }
        }
    }
}