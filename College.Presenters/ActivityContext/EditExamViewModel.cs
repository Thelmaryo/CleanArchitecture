﻿using College.Presenters.Shared;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.ActivityContext
{
    public class EditExamViewModel
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string DisciplineId { get; set; }
        [Display(Name = "Acadêmico")]
        public string Student { get; set; }
        [Display(Name = "Prova 1")]
        public decimal Exam1 { get; set; }
        [Display(Name = "Prova 2")]
        public decimal Exam2 { get; set; }
        [Display(Name = "Prova 3")]
        public decimal Exam3 { get; set; }
        [Display(Name = "Exame Final")]
        public decimal FinalExam { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}