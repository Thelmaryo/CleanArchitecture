using College.Entities.CourseContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.CourseContext.Repositories
{
    public interface IDisciplineRepository
    {
        void Create(Discipline discipline);
        void Update(Discipline discipline);
        void Delete(Guid id);
        Discipline Get(Guid id);
        IEnumerable<Discipline> List();
        IEnumerable<Discipline> GetByProfessor(Guid id);
        IEnumerable<Discipline> GetByEnrollment(Guid id);
    }
}
