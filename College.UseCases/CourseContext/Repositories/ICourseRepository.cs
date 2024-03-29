﻿using College.Entities.CourseContext.Entities;
using System.Collections.Generic;

namespace College.UseCases.CourseContext.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> List();
    }
}
