using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EnrollmentContext.Inputs
{
    public class EnrollmentInputGetByStudent : ICommand
    {
        public Guid StudentId { get; set; }
    }
}
