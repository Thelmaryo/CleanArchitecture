using College.Entities.CourseContext.Entities;
using College.Infra.DataSource;
using College.UseCases.CourseContext.Repositories;
using Dapper;
using System.Collections.Generic;

namespace College.Infra.EnrollmentContext
{
    public class CourseRepository : ICourseRepository
    {
        IDB _db;
        string sql;
        public CourseRepository(IDB db)
        {
            _db = db;
        }
        public IEnumerable<Course> List()
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [Id], [Name]	FROM [Course] ";
                var courses = db.Query<Course>(sql);
                return courses;
            }
        }
    }
}
