using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.CourseContext.Inputs
{
    public class DisciplineInputListByStudent : ICommand
    {
        public Guid StudentId { get; set; }
    }
}
