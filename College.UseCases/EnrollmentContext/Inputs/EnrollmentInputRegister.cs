using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EnrollmentContext.Inputs
{
    public class EnrollmentInputRegister : ICommand
    {
        // Name Student
        public string Name { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        // EStatusEnrollment
        public int Status { get; set; }
    }
}
