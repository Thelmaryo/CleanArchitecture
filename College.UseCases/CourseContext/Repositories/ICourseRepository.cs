using College.Entities.CourseContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.CourseContext.Repositories
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> List();
    }
}
