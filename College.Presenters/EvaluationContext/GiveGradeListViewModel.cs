﻿using College.Presenters.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace College.Presenters.EvaluationContext
{
    public class GiveGradeListViewModel
    {
        public Guid DisciplineId { get; set; }
        public string ActivityName { get; set; }
        public decimal ActivityValue { get; set; }
        public List<GiveGradeListItem> Students = new List<GiveGradeListItem>();
        public BackButton BackButton => new BackButton();
    }

    public class GiveGradeListItem
    {
        public Guid ActivityId { get; set; }
        public Guid StudentId { get; set; }
        [Display(Name = "Acadêmico")]
        public string Student { get; set; }
        [Display(Name = "Nota")]
        public string Grade { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
    }
}
