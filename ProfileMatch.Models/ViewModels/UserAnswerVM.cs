﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMatch.Models.Models;

namespace ProfileMatch.Models.ViewModels
{
  public  class UserAnswerVM
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUserVM ApplicationUser { get; set; }
        public AnswerOption AnswerOption { get; set; }
        public int AnswerOptionId { get; set; }
        public bool IsConfirmed { get; set; }
        public string SupervisorId { get; set; }
    }
}
