using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EnrollmentContext.Inputs
{
    public class EnrollmentInputRegister : ICommand
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public int EnrollmentStatus { get; set; }
        public Guid[] Disciplines { get; set; }
    }
}
