using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.EvaluationContext.Inputs
{
    public class ActivityInputGetByDiscipline : ICommand
    {
        public Guid StudentId { get; set; }
        public Guid DisciplineId { get; set; }
    }
}
