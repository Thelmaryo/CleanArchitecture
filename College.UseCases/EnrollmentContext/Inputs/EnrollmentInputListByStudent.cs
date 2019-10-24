using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EnrollmentContext.Inputs
{
    public class EnrollmentInputListByStudent : ICommand
    {
        public Guid StudentId { get; set; }
    }
}
