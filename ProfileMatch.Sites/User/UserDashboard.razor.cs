﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Services;

namespace ProfileMatch.Sites.User
{
    public partial class UserDashboard : ComponentBase
    {
        [Inject]
        private IStringLocalizer<LanguageService> L { get; set; }
    }
}