using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.CourseContext.Result
{
    public class DisciplineResultQueryGet : ICommandResult
    {
        public IDictionary<string, string> Notifications { get; set; }

        public Discipline Discipline { get; set; }
    }
}
