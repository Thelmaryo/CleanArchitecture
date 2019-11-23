using College.Entities.CourseContext.Entities;
using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.CourseContext.Result
{
    public class CourseResultQueryList : IQueryResult
    {
        public IEnumerable<Course> Courses { get; set; }
    }
}
