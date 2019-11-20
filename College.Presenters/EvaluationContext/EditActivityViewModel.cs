using College.Presenters.Shared;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.EvaluationContext
{
    public class EditActivityViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Activity")]
        public string Description { get; set; }
        [Display(Name = "Valor")]
        public decimal Value { get; set; }
        [Display(Name = "Data")]
        public string Date { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
