using College.Presenters.Shared;
using System.Collections.Generic;

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
        public string Discipline { get; set; }
        public decimal Grade { get; set; }
        public decimal FinalExam { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public LinkButton GradesButton => new LinkButton("Notas");

    }
}
