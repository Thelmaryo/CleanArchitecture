using College.Entities.ProfessorContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.ProfessorContext.Repositories
{
    public interface IProfessorRepository
    {
        void Create(Professor professor);
        void Update(Professor professor);
        void Disable(Guid id);
        Professor Get(Guid id);
        IEnumerable<Professor> List();
    }
}
