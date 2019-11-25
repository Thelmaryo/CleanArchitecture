using College.Entities.CourseContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.CourseContext.Repositories
{
    public interface IProfessorRepository
    {
        int GetWorkload(Guid professorId);
        IEnumerable<Professor> List();
    }
}
