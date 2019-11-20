using College.Entities.StudentContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.StudentContext.Repositories
{
    public interface IStudentRepository
    {
        void Create(Student student);
        void Update(Student student);
        void Delete(Guid id);
        Student Get(Guid id);
        Student Get(string CPF);
        IEnumerable<Student> GetByDiscipline(Guid id);
        IEnumerable<Student> List();
    }
}
