using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EnrollmentContext.Inputs
{
    public class EnrollmentInputDeny : ICommand
    {
        public Guid EnrollmentId { get; set; }
    }
}
