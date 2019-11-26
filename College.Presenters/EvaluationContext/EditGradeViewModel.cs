using College.Presenters.Shared;
using System;

namespace College.Presenters.EvaluationContext
{
    public class EditGradeViewModel
    {
        public decimal Grade { get; set; }
        public decimal Value { get; set; }
        public Guid StudentId { get; set; }
        public string Student { get; set; }
        public Guid ActivityId { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
