using College.Entities.CourseContext.Entities;
using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.Infra.CourseContext;
using College.Infra.DataSource;
using College.Infra.ProfessorContext;
using College.Infra.Tests.DataSource;
using College.UseCases.CourseContext.Repositories;
using College.UseCases.ProfessorContext.Repositories;
using Cryptography.EncryptContext;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;

namespace College.Infra.Tests.ProfessorContext
{
    [TestClass]
    public class ProfessorRepositoryTest
    {
        Entities.ProfessorContext.Entities.Professor professor;
        Discipline discipline;
        Course course;
        string sql;
        UseCases.ProfessorContext.Repositories.IProfessorRepository _PREP;
        IDisciplineRepository _DREP;
        [TestInitialize]
        public void Init()
        {
            _PREP = new Infra.ProfessorContext.ProfessorRepository(new MSSQLDB(new DBConfiguration()));
            _DREP = new DisciplineRepository(new MSSQLDB(new DBConfiguration()));
            var db = new SqlConnection(new DBConfiguration().StringConnection);


            string CPF = "034.034.034-00";
            string password = new Encryptor().Encrypt(CPF.Replace("-", "").Replace(".", ""), out string salt);
            professor = new Entities.ProfessorContext.Entities.Professor("Thelmaryo", "Vieira Lima", CPF, "thelmaryoTest@hotmail.com", "123", EDegree.Master, password, salt);
            _PREP.Create(professor);

            course = new Course("Psicologia");
            sql = "INSERT INTO [Course] ([Id], [Name]) VALUES (@Id, @Name)";
            db.Execute(sql, param: new { course.Id, course.Name });

            discipline = new Discipline("Psicologia", new Course(course.Id), new Entities.CourseContext.Entities.Professor(professor.Id), 20, 1, 0);
            _DREP.Create(discipline);
        }

        [TestMethod]
        public void ShouldCreateAndGetAProfessor()
        {
            var professorDB = _PREP.Get(professor.Id);
            Assert.IsNotNull(professorDB);
            Assert.AreEqual(professor.Name, professorDB.Name);
            Assert.AreEqual(professor.CPF.Number, professorDB.CPF.Number);
            Assert.AreEqual(professor.Id, professorDB.Id);
            Assert.IsTrue(professor.Active);
        }

        [TestMethod]
        public void ShouldUpdateAProfessor()
        {
            professor.UpdateEntity("Abmael", "Silva", "879.619.330-18", "thelmaryoTest@hotmail.com", "1234-1234", EDegree.Doctor);

            _PREP.Update(professor);
            var professorDB = _PREP.Get(professor.Id);
            Assert.IsNotNull(professorDB);
            Assert.AreEqual("Abmael", professorDB.FirstName);
            Assert.AreEqual("879.619.330-18", professorDB.CPF.Number);
            Assert.AreEqual(professor.Id, professorDB.Id);
            Assert.IsTrue(professor.Active);
        }

        [TestMethod]
        public void ShouldDisableAProfessor()
        {
            _PREP.Disable(professor.Id);
            var professorDB = _PREP.Get(professor.Id);
            Assert.IsNotNull(professorDB);
            Assert.IsFalse(professorDB.Active);
        }

        [TestMethod]
        public void ShouldSingleIdAProfessor()
        {
            var professorsDB = _PREP.List();
            Assert.IsNotNull(professorsDB);

            Guid professorDBId = Guid.NewGuid();
            
            foreach (var professorDB in professorsDB)
            {
                Assert.IsFalse(professorDBId == professorDB.Id);
                professorDBId = professorDB.Id;
            }
        }

        [TestMethod]
        public void ShouldSingleCPFAProfessor()
        {
            var professorsDB = _PREP.List();
            Assert.IsNotNull(professorsDB);

            string professorDBCPF = "";
            foreach (var professorDB in professorsDB)
            {
                Assert.IsFalse(professorDBCPF == professorDB.CPF.Number);
                professorDBCPF = professorDB.CPF.Number;
            }
        }

        [TestCleanup]
        public void Clean()
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Delete Discipline
            _DREP.Delete(discipline.Id);

            // Delete Course
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { course.Id });

            // Delete Professor
            sql = "DELETE FROM [Professor] WHERE Id = @Id";
            db.Execute(sql, param: new { professor.Id });

            // Delete User
            sql = "DELETE FROM [User] WHERE Id = @Id";
            db.Execute(sql, param: new { professor.Id });
        }
    }
}
