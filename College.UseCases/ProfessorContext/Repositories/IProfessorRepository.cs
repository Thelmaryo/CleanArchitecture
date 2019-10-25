using College.Entities.ProfessorContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.ProfessorContext.Repositories
{
    public interface IProfessorRepository
    {
        public void Create(Professor professor);
        public void Update(Professor professor);
        public void Disable(Guid id);
        public int GetWorkload(Guid professorId);
        public Professor Get(Guid id);
        public IEnumerable<Professor> List();
    }
}
