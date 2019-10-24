using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.ProfessorContext.Inputs
{
    public class ProfessorInputGetWorkload : ICommand
    {
        public Guid ProfessorId { get; set; }
    }
}
