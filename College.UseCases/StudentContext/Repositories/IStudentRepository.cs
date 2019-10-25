using College.Entities.StudentContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.StudentContext.Repositories
{
    public interface IStudentRepository
    {
        public void Create(Student student);
        public void Update(Student student);
        public void Delete(Guid id);
        public Student Get(Guid id);
        public Student Get(string CPF);
        public IEnumerable<Student> GetByDiscipline(Guid id);
        public IEnumerable<Student> List();
    }
}
