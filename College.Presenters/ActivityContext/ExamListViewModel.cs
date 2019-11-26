using College.Presenters.Shared;
using System.Collections.Generic;

namespace College.Presenters.ActivityContext
{
    public class ExamListViewModel
    {
        public string DisciplineName { get; set; }
        public IEnumerable<ExamListItem> Students { get; set; }
    }

    public class ExamListItem
    {
        public string Id { get; set; }
        public string Student { get; set; }
        public decimal Exam1 { get; set; }
        public decimal Exam2 { get; set; }
        public decimal Exam3 { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
    }
}
