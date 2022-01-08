﻿
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Models.ViewModels;
using ProfileMatch.Repositories;

using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ProfileMatch.Components.Dialogs
{
    public partial class UserOpenQuestionDialog
    {
       [Inject]  private  IStringLocalizer<LanguageService> L { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

       
        [Inject] private ISnackbar Snackbar { get; set; }
        UserOpenAnswer EditUserNote;
        [Parameter] public UserAnswerVM UserNoteVM { get; set; }
        string TempDescription;
        bool IsDisplayed;
        bool exists;
        private MudTextField<string> multilineReference;

        protected override async Task OnInitializedAsync()
        {
            exists = await UserNoteRepository.ExistById(UserNoteVM.UserId, UserNoteVM.AnswerId);
            if (exists)
            {
                EditUserNote = await UserNoteRepository.GetById(UserNoteVM.UserId, UserNoteVM.AnswerId);
            }
            else
            {
                EditUserNote = new()
                {
                    IsDisplayed = UserNoteVM.IsDisplayed,
                    OpenQuestionId = UserNoteVM.AnswerId,
                    ApplicationUserId = UserNoteVM.UserId
                };
            }
            TempDescription = EditUserNote.UserAnswer;
            IsDisplayed = EditUserNote.IsDisplayed;
        }

        [Inject] DataManager<UserOpenAnswer, ApplicationDbContext> UserNoteRepository { get; set; }

        private MudForm Form;

        private void Cancel()
        {
            MudDialog.Cancel();
            Snackbar.Add(@L["Operation cancelled"], Severity.Warning);
        }

        protected async Task HandleSave()
        {
            await Form.Validate();
            if (Form.IsValid)
            {
                EditUserNote.IsDisplayed = IsDisplayed;
                EditUserNote.UserAnswer = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add(@L[$"There was an error: {ex.Message}"], Severity.Error);
                }
                UserNoteVM.IsDisplayed = IsDisplayed;
                UserNoteVM.UserDescription = TempDescription;
                MudDialog.Close(DialogResult.Ok(UserNoteVM));
            }
        }

        private async Task Save()
        {
            if (!exists)
            {
                var result = await UserNoteRepository.Insert(EditUserNote);
                Snackbar.Add(@L["Answer added"], Severity.Success);
            }
            else
            {
                var result = await UserNoteRepository.Update(EditUserNote);
                Snackbar.Add(@L["Answer updated"], Severity.Success);
            }
        }

        private IEnumerable<string> MaxCharacters(string ch)
        {

            if (!string.IsNullOrEmpty(ch) && 199 < ch?.Length)
                yield return @L["Max 200 characters"];
            
        }
    }
}
