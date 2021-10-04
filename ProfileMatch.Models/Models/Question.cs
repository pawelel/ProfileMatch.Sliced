﻿using System.Collections.Generic;

namespace ProfileMatch.Models.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<AnswerOption> AnswerOptions { get; set; } = new();
    }
}