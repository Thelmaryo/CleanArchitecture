using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.CourseContext.Inputs
{
    public class DisciplineInputListById : ICommand
    {
        public Guid DisciplineId { get; set; }
    }
}
