using College.Presenters.Shared;

namespace College.Presenters.ActivityContext
{
    public class EditExamViewModel
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string DisciplineId { get; set; }
        public string Student { get; set; }
        public decimal Exam1 { get; set; }
        public decimal Exam2 { get; set; }
        public decimal Exam3 { get; set; }
        public decimal FinalExam { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
