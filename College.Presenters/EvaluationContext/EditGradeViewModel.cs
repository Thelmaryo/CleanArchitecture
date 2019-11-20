using College.Presenters.Shared;
using System.ComponentModel.DataAnnotations;

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
