using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.CourseContext.Inputs
{
    public class CourseInputGet : ICommand
    {
        public Guid CourseId { get; set; }
    }
}
