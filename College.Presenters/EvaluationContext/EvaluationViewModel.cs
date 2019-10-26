using System.ComponentModel.DataAnnotations;

namespace College.Presenters.EvaluationContext
{
    public class EvaluationViewModel
    {
        // Student Name
        [Display(Name = "Acadêmico")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Categoria")]
        public decimal Grade { get; set; }

        [Display(Name = "Nota")]
        public decimal Value { get; set; }
    }
}
