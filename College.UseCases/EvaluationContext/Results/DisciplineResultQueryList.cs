using College.Entities.EvaluationContext.Entities;
using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.EvaluationContext.Results
{
    public class DisciplineResultQueryList : IQueryResult
    {
        public IEnumerable<Discipline> Disciplines { get; set; }
    }
}
