﻿using College.Presenters.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace College.Presenters.EvaluationContext
{
    public class ExamListViewModel
    {
        public string DisciplineName { get; set; }
        public IEnumerable<ExamListItem> Students { get; set; }
    }

    public class ExamListItem
    {
        public string Id { get; set; }
        [Display(Name = "Acadêmico")]
        public string Student { get; set; }
        [Display(Name = "Prova 1")]
        public decimal Exam1 { get; set; }
        [Display(Name = "Prova 2")]
        public decimal Exam2 { get; set; }
        [Display(Name = "Prova 3")]
        public decimal Exam3 { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
    }
}
