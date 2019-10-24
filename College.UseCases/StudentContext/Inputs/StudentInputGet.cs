using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.StudentContext.Inputs
{
    public class StudentInputGet : ICommand
    {
        // Id Course
        public Guid StudentId { get; set; }
    }
}
