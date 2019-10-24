using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.ProfessorContext.Result
{
    public class ProfessorResultQueryGetWorkload : ICommandResult
    {
        public IDictionary<string, string> Notifications { get; set; }

        public int Workload { get; set; }
    }
}
