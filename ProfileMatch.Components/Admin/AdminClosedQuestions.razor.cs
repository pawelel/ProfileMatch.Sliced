﻿using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using MudBlazor;

using ProfileMatch.Components.Dialogs;
using ProfileMatch.Contracts;
using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class AdminClosedQuestions : ComponentBase
    {
        [Inject] private IDialogService DialogService { get; set; }

        [Inject] DataManager<Category, ApplicationDbContext> CategoryRepository { get; set; }

        [Inject] DataManager<Question, ApplicationDbContext> QuestionRepository { get; set; }


        private bool loading;
        [Parameter] public int Id { get; set; }
        private List<Question> questions = new();
        private List<Question> questions1;
        private List<Category> categories;
        private IEnumerable<string> Cats { get; set; } = new HashSet<string>() { };
        private IEnumerable<string> Quests { get; set; } = new HashSet<string>() { };
        public bool ShowDetails { get; set; }

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            categories = await CategoryRepository.Get();
            questions = await QuestionRepository.Get(include: src => src.Include(q => q.Category).Include(q => q.AnswerOptions));
            questions1 = questions;
            loading = false;
        }





        private string searchString1 = "";
        private Question selectedItem1 = null;

        private bool FilterFunc1(Question question) => FilterFunc(question, searchString1);

        private static bool FilterFunc(Question question, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (question.Category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (question.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private List<Question> GetQuestions()
        {
            if (!Cats.Any())
            {
                questions1 = questions;
            }
            else
            {
                questions1 = (from q in questions
                              from c in Cats
                              where q.Category.Name == c
                              select q).ToList();
            }
            if (Quests.Any())
            {
                questions1 = (from q in questions1
                              from a in Quests
                              where q.Name == a
                              select q).ToList();
            }

            return questions1;
        }

        private async Task QuestionDialog(Question question)
        {
            var parameters = new DialogParameters { ["Q"] = question };
            var dialog = DialogService.Show<AdminQuestionDialog>($"Edytuj pytanie: {question.Name}", parameters);
            await dialog.Result;
        }

        private async Task QuestionDisplay(Question question)
        {
            DialogOptions maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters { ["Q"] = question };
            var dialog = DialogService.Show<AdminQuestionDisplay>($"Wyświetl pytanie", parameters, maxWidth);
            await dialog.Result;
        }

        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}