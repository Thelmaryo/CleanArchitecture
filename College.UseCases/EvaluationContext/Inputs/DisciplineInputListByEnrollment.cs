using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EvaluationContext.Inputs
{
    public class DisciplineInputListByEnrollment : ICommand
    {
        public Guid EnrollmentId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime SemesterBegin { get; set; }
        public DateTime SemesterEnd { get; set; }
    }
}
