﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Services;

namespace ProfileMatch.Sites.Admin
{
    public partial class AdminDashboard : ComponentBase
    {
        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}