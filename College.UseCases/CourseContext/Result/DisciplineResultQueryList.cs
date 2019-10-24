using College.Entities.CourseContext.Entities;
using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.CourseContext.Result
{
    public class DisciplineResultQueryList : IQueryResult
    {
        public IEnumerable<Discipline> Discipline { get; set; }
    }
}
