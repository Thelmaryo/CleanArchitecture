using College.Presenters.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.EvaluationContext
{
    public class GradeListViewModel
    {
        public string ActivityName { get; set; }
        public decimal ActivityValue { get; set; }
        public IEnumerable<GradeListItem> Students { get; set; }
        public BackButton BackButton => new BackButton();
    }

    public class GradeListItem
    {
        public string ActivityId { get; set; }
        public string StudentId { get; set; }
        [Display(Name = "Acadêmico")]
        public string Student { get; set; }
        [Display(Name = "Nota")]
        public string Grade { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
    }
}
