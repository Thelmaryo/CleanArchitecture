using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.CourseContext.Result
{
    public class DisciplineResultQueryList : ICommandResult
    {
        public IDictionary<string, string> Notifications { get; set; }

        public IEnumerable<Discipline> Discipline { get; set; }
    }
}
