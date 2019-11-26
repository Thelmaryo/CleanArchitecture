using College.Presenters.Shared;
using System;

namespace College.Presenters.EvaluationContext
{
    public class EditExamViewModel
    {
        public Guid StudentId { get; set; }
        public Guid DisciplineId { get; set; }
        public string Student { get; set; }
        public string Exam1 { get; set; }
        public string Exam2 { get; set; }
        public string Exam3 { get; set; }
        public string FinalExam { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
