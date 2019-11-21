using College.Entities.ProfessorContext.Entities;
using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.ProfessorContext.Result
{
    public class ProfessorResultQueryList : IQueryResult
    {
        public IEnumerable<Professor> Professors { get; set; }
    }
}
