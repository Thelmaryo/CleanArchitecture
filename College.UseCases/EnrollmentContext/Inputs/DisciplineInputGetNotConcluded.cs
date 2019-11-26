using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.EnrollmentContext.Inputs
{
    public class DisciplineInputGetNotConcluded : ICommand
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
    }
}
