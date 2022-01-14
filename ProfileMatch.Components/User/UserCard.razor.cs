﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

using ProfileMatch.Models.Models;
using ProfileMatch.Services;

using System.Threading.Tasks;

namespace ProfileMatch.Components.User
{
    public partial class UserCard : ComponentBase
    {
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        [CascadingParameter] public ApplicationUser CurrentUser { get; set; }

    }
}