using College.Entities.CourseContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.CourseContext.Repositories
{
    public interface IDisciplineRepository
    {
        public void Create(Discipline discipline);
        public void Edit(Discipline discipline);
        public void Delete(Guid id);
        public Discipline Get(Guid id);
        public IEnumerable<Discipline> List();
        public IEnumerable<Discipline> GetByCourse(Guid id);
        public IEnumerable<Discipline> GetByProfessor(Guid id);
        public IEnumerable<Discipline> GetByEnrollment(Guid id);
        public IEnumerable<Discipline> GetConcluded(Guid studentId);
    }
}
