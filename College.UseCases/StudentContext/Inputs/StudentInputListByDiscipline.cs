using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.StudentContext.Inputs
{
    public class StudentInputListByDiscipline : ICommand
    {
        public Guid DisciplineId { get; set; }
    }
}
