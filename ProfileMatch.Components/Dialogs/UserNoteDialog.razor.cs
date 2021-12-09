﻿using Microsoft.AspNetCore.Components;
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
    public partial class UserNoteDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private static IEnumerable<string> MaxCharacters(string ch)
        {
            if (!string.IsNullOrEmpty(ch) && 199 < ch?.Length)
                yield return "Max 200 characters";
        }
        [Inject] private ISnackbar Snackbar { get; set; }
        UserNote EditUserNote;
        [Parameter] public UserNoteVM UserNoteVM { get; set; }
        string TempDescription;
        bool IsDisplayed;
        bool exists;
        protected override async Task OnInitializedAsync()
        {
            exists = await UserNoteRepository.ExistById(UserNoteVM.UserId, UserNoteVM.NoteId);
            if (exists)
            {
                EditUserNote = await UserNoteRepository.GetById(UserNoteVM.UserId, UserNoteVM.NoteId);
            }
            else
            {
                EditUserNote = new()
                {
                    IsDisplayed = UserNoteVM.IsDisplayed,
                    NoteId = UserNoteVM.NoteId,
                    ApplicationUserId = UserNoteVM.UserId
                };
            }
            TempDescription = EditUserNote.Description;
            IsDisplayed = EditUserNote.IsDisplayed;
        }

        [Inject] DataManager<UserNote, ApplicationDbContext> UserNoteRepository { get; set; }

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
                EditUserNote.IsDisplayed = IsDisplayed;
                EditUserNote.Description = TempDescription;
                try
                {
                    await Save();
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"There was an error: {ex.Message}", Severity.Error);
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
                Snackbar.Add($"Answer added", Severity.Success);
            }
            else
            {
                var result = await UserNoteRepository.Update(EditUserNote);
                Snackbar.Add($"Answer updated", Severity.Success);
            }
        }

        [Inject] private IStringLocalizer<LanguageService> L { get; set; }
    }
}
