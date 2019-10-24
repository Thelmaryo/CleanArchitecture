using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.CourseContext.Inputs
{
    public class DisciplineInputGet : ICommand
    {
        public Guid DisciplineId { get; set; }
    }
}
