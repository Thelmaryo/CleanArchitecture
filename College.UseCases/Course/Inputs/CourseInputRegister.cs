using College.UseCases.Commands;
using System;

namespace College.UseCases.Course.Inputs
{
    public class CourseInputRegister : ICommand
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
    }
}
