using College.Entities.ProfessorContext.Entities;
using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.ProfessorContext.Result
{
    public class ProfessorResultQueryGet : ICommandResult
    {
        public IDictionary<string, string> Notifications { get; set; }

        public Professor Professor { get; set; }
    }
}
