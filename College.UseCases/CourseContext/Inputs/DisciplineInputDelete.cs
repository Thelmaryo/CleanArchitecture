using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.CourseContext.Inputs
{
    public class DisciplineInputDelete : ICommand
    {
        public Guid DisciplineId { get; set; }
    }
}
