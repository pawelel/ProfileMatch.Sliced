﻿using Microsoft.AspNetCore.Components;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Components.User
{
    public partial class LoginDisplay : ComponentBase
    {
        [Parameter] public ApplicationUser CurrentUser { get; set; }
    }
}