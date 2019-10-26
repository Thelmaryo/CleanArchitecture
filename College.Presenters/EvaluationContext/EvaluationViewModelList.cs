using System.ComponentModel.DataAnnotations;

namespace College.Presenters.EvaluationContext
{
    public class EvaluationViewModelList
    {
        // Student Name
        [Display(Name = "Acadêmico")]
        public string Name { get; set; }
    }
}
