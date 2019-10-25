using College.UseCases.Shared;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.ActivityContext.Inputs
{
    public class ActivityInputGetByDiscipline : ICommand
    {
        public Guid DisciplineId { get; set; }
        public Semester Semester { get; set; }
    }
}
