using College.UseCases.Shared.Commands;
using College.UseCases.CourseContext.Inputs;
using College.UseCases.CourseContext.Repositories;
using College.UseCases.CourseContext.Result;

namespace College.UseCases.CourseContext.Queries
{
    public class CourseQueryHandler : IQueryHandler<CourseInputGet, CourseResultQueryList>
    {
        private readonly ICourseRepository _CREP;

        public CourseQueryHandler(ICourseRepository CREP)
        {
            _CREP = CREP;
        }
        public CourseResultQueryList Handle(CourseInputGet command)
        {
            throw new System.NotImplementedException();
        }
        public CourseResultQueryList Handle()
        {
            var result = new CourseResultQueryList();
            result.Course = _CREP.List();
            return result;
        }
    }
}
