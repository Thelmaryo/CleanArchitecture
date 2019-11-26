using College.Presenters.Shared;
using System;
using System.Collections.Generic;

namespace College.Presenters.EvaluationContext
{
    public class ExamListViewModel
    {
        public Guid DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public List<ExamListItem> Students { get; set; } = new List<ExamListItem>();
    }

    public class ExamListItem
    {
        public Guid StudentId { get; set; }
        public string Student { get; set; }
        public decimal Exam1 { get; set; }
        public decimal Exam2 { get; set; }
        public decimal Exam3 { get; set; }
        public decimal FinalExam { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
    }
}
