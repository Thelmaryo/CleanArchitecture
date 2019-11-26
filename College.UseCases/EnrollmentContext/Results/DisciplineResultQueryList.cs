using College.Entities.EnrollmentContext.Entities;
using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.EnrollmentContext.Result
{
    public class DisciplineResultQueryList : IQueryResult
    {
        public IEnumerable<Discipline> Disciplines { get; set; }
    }
}
