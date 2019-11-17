using College.Presenters.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.EvaluationContext
{
    public class GradeDetailsViewModel
    {
        public string EnrollmentId { get; set; }
        public IEnumerable<GradeDetailsActivity> Activities { get; set; }
        public BackButton BackButton => new BackButton();
    }

    public class GradeDetailsActivity
    {
        [Display(Name = "Atividade")]
        public string Activity { get; set; }
        [Display(Name = "Valor")]
        public decimal Value { get; set; }
        [Display(Name = "Nota")]
        public decimal Grade { get; set; }
    }
}
