using College.Entities.CourseContext.Entities;
using College.UseCases.Shared.Commands;

namespace College.UseCases.CourseContext.Result
{
    public class DisciplineResultQueryGet : IQueryResult
    {
        public Discipline Discipline { get; set; }
    }
}
