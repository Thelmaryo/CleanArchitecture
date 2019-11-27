using College.Presenters.Shared;
using System;
using System.Collections.Generic;

namespace College.Presenters.EvaluationContext
{
    public class ShowGradesViewModel
    {
        public Guid EnrollmentId { get; set; }
        public decimal Exam1 { get; set; }
        public decimal Exam2 { get; set; }
        public decimal Exam3 { get; set; }
        public IEnumerable<ShowGradesItem> Activities { get; set; }
        public BackButton BackButton => new BackButton();
    }

    public class ShowGradesItem
    {
        public string Activity { get; set; }
        public decimal Grade { get; set; }
        public decimal Value { get; set; }
    }
}
