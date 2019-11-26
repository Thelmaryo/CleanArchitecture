using College.Presenters.Shared;
using System.Collections.Generic;

namespace College.Presenters.EvaluationContext
{
    public class GradeDetailsViewModel
    {
        public string EnrollmentId { get; set; }
        public IEnumerable<GradeDetailsActivity> Activities { get; set; }
        public BackButton BackButton => new BackButton();
    }

    public class GradeDetailsActivity
    {
        public string Activity { get; set; }
        public decimal Value { get; set; }
        public decimal Grade { get; set; }
    }
}
