using College.Presenters.Shared;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.EvaluationContext
{
    public class DeleteActivityViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Disciplina")]
        public string Disciplina { get; set; }

        [Display(Name = "Activity")]
        public string Description { get; set; }
        [Display(Name = "Valor")]
        public decimal Value { get; set; }
        [Display(Name = "Data")]
        public string Date { get; set; }
        public BackButton SaveButton => new BackButton();
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
