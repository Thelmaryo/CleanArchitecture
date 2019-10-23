using College.UseCases.Commands;
using System;

namespace College.UseCases.CourseContext.Inputs
{
    public class CourseInputRegister : ICommand
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
    }
}
