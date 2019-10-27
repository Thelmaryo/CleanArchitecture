using College.Presenters.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace College.Presenters.EvaluationContext
{
    public class EditGradeViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Nota")]
        public string Grade { get; set; }
        public string Student { get; set; }
        public string ActivityId { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
