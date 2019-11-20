using College.Presenters.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.EvaluationContext
{
    public class GradeListViewModel
    {
        public IEnumerable<GiveGradeListItem> Students { get; set; }
    }

    public class GradeListItem
    {
        public string DisciplineId { get; set; }
        public string EnrollmentId { get; set; }
        [Display(Name = "Disciplina")]
        public string Discipline { get; set; }
        [Display(Name = "Nota")]
        public decimal Grade { get; set; }
        [Display(Name = "Exame Final")]
        public decimal FinalExam { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public LinkButton GradesButton => new LinkButton("Notas");

    }
}
