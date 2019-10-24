using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EnrollmentContext.Inputs
{
    public class EnrollmentInputGet : ICommand
    {
        public Guid EnrollmentId { get; set; }
    }
}
