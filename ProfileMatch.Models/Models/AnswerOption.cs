﻿using System.ComponentModel.DataAnnotations;

namespace ProfileMatch.Models.Models
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int Level { get; set; }
        public Question Question { get; set; }
        public string Description { get; set; }
    }
}