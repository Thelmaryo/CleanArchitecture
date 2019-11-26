using College.UseCases.Shared.Commands;
using College.UseCases.CourseContext.Inputs;
using College.UseCases.CourseContext.Repositories;
using College.UseCases.CourseContext.Result;
using System.Linq;

namespace College.UseCases.CourseContext.Queries
{
    public class CourseQueryHandler : IQueryHandler<CourseInputList, CourseResultQueryList>
    {
        private readonly ICourseRepository _CREP;

        public CourseQueryHandler(ICourseRepository CREP)
        {
            _CREP = CREP;
        }
        public CourseResultQueryList Handle(CourseInputList command)
        {
            var result = new CourseResultQueryList();
            result.Courses = _CREP.List().OrderBy(x=>x.Name);
            return result;
        }
    }
}
