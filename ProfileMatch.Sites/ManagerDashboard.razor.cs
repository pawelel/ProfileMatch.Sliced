﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Services;

namespace ProfileMatch.Sites
{
    public partial class ManagerDashboard : ComponentBase
    {
        [Parameter] public int ActiveIndex { get; set; }
    }
}