using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.CourseContext.Inputs
{
    public class DisciplineInputGetByProfessor : ICommand
    {
        public Guid ProfessorId { get; set; }
    }
}
