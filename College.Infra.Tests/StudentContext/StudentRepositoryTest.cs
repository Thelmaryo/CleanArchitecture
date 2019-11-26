using College.Entities.EnrollmentContext.Entities;
using College.Entities.EnrollmentContext.Enumerators;
using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.Entities.StudentContext.Entities;
using College.Infra.CourseContext;
using College.Infra.DataSource;
using College.Infra.EnrollmentContext;
using College.Infra.ProfessorContext;
using College.Infra.StudentContext;
using College.Infra.Tests.DataSource;
using College.UseCases.CourseContext.Repositories;
using College.UseCases.EnrollmentContext.Repositories;
using College.UseCases.ProfessorContext.Repositories;
using College.UseCases.StudentContext.Repositories;
using Cryptography.EncryptContext;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;

namespace College.Infra.Tests.StudentContext
{
    [TestClass]
    public class StudentRepositoryTest
    {
        string sql;
        Entities.StudentContext.Entities.Student student;
        Entities.CourseContext.Entities.Discipline discipline;
        Enrollment enrollment;
        Professor professor;
        Guid StudentDisciplineId;
        IStudentRepository _SREP;
        IEnrollmentRepository _EREP;
        UseCases.CourseContext.Repositories.IDisciplineRepository _DREP;
        UseCases.ProfessorContext.Repositories.IProfessorRepository _PREP;
        Course course;
        // IEncryptor _encryptor;
        [TestInitialize]
        public void Init()
        {
            _SREP = new StudentRepository(new MSSQLDB(new DBConfiguration()));
            _EREP = new EnrollmentRepository(new MSSQLDB(new DBConfiguration()));
            _DREP = new Infra.CourseContext.DisciplineRepository(new MSSQLDB(new DBConfiguration()));
            _PREP = new Infra.ProfessorContext.ProfessorRepository(new MSSQLDB(new DBConfiguration()));
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Create Course
            course = new Course(Guid.NewGuid());
            sql = "INSERT INTO [Course] ([Id], [Name]) VALUES (@Id, 'LTP')";
            db.Execute(sql, param: new { Id = course.CourseId });

            // Create Student
            string CPF = "117.400.002-34";
            string password = new Encryptor().Encrypt(CPF.Replace("-", "").Replace(".", ""), out string salt);
            student = new Entities.StudentContext.Entities.Student(course, DateTime.Now, "Abmael", "Araujo", CPF, "abmaelTest@gmail.com", "1234-1234", "F", "França", "AraguainaSul", "Norte", password, salt);
            _SREP.Create(student);

            // Create Enrellment
            var _student = new Entities.EnrollmentContext.Entities.Student(student.Id);
            enrollment = new Enrollment(_student, Convert.ToDateTime("2000-08-01"), Convert.ToDateTime("2025-12-01"), EStatusEnrollment.Confirmed);
            _EREP.Create(enrollment);

            // Create Professor
            professor = new Professor("Thelmaryo", "Vieira Lima", "034.034.034-00", "thelmaryoTest@hotmail.com", "123", EDegree.Master, "123", "123");
            _PREP.Create(professor);

            // Create Discipline
            discipline = new Entities.CourseContext.Entities.Discipline("Psicologia", new Entities.CourseContext.Entities.Course(course.CourseId), new Entities.CourseContext.Entities.Professor(professor.Id), 20, 1,0);
            _DREP.Create(discipline);

            // Create StudentDiscipline
            StudentDisciplineId = Guid.NewGuid();
            sql = "INSERT INTO [dbo].[StudentDiscipline] ([Id],[EnrollmentId],[DisciplineId],[Status]) VALUES(@Id, @EnrollmentId, @DisciplineId, @Status);";
            db.Execute(sql, param: new { Id = StudentDisciplineId, EnrollmentId = enrollment.Id, DisciplineId = discipline.Id, Status = 0 });
        }
        [TestMethod]
        public void ShouldCreateAndGetIdAStudent()
        {
            var studentDB = _SREP.Get(student.Id);
            Assert.IsNotNull(studentDB);
            Assert.AreEqual(student.FirstName, studentDB.FirstName);
            Assert.IsTrue(student.Active);
            Assert.AreEqual(student.CPF.Number, studentDB.CPF.Number);
            Assert.AreEqual(student.Id, studentDB.Id);
        }

        [TestMethod]
        public void ShouldCreateAndGetCPFAStudent()
        {
            var studentDB = _SREP.Get(student.CPF.Number);
            Assert.IsNotNull(studentDB);
            Assert.AreEqual(student.FirstName, studentDB.FirstName);
            Assert.AreEqual(student.CPF.Number, studentDB.CPF.Number);
            Assert.AreEqual(student.Id, studentDB.Id);
            Assert.IsTrue(student.Active);
        }

        [TestMethod]
        public void ShouldUpdateAStudent()
        {
            var date = Convert.ToDateTime(DateTime.Now.ToString("dd\\/MM\\/yyyy"));
            student.UpdateEntity(student.Course, date, "Abmael", "Silva", "abmaelsilvaTest@gmail.com", "5678-5678", "M", "Potugal", "Cacique", "Sul", student.Id);

            _SREP.Update(student);
            var professorDB = _SREP.Get(student.Id);
            Assert.IsNotNull(professorDB);
            Assert.AreEqual("Abmael", professorDB.FirstName);
            Assert.IsTrue(student.Active);
        }

        [TestMethod]
        public void ShouldGetByDisciplineAStudent()
        {
            var studentsDB = _SREP.GetByDiscipline(discipline.Id);
            Assert.IsNotNull(studentsDB);
            foreach (var studentDB in studentsDB)
            {
                Assert.IsTrue(student.Active);
            }
        }

        [TestMethod]
        public void ShouldListAStudent()
        {
            var studentsDB = _SREP.List();
            Assert.IsNotNull(studentsDB);
            foreach (var studentDB in studentsDB)
            {
                Assert.IsTrue(student.Active);
            }
        }

        [TestMethod]
        public void ShouldSingleIdAStudent()
        {
            var studentsDB = _SREP.List();
            Assert.IsNotNull(studentsDB);

            Guid studentDBId = Guid.NewGuid();

            foreach (var studentDB in studentsDB)
            {
                Assert.IsFalse(studentDBId == studentDB.Id);
                studentDBId = studentDB.Id;
            }
        }

        [TestMethod]
        public void ShouldSingleCPFAStudent()
        {
            var studentsDB = _SREP.List();
            Assert.IsNotNull(studentsDB);

            string studentDBCPF = "";

            foreach (var studentDB in studentsDB)
            {
                Assert.IsFalse(studentDBCPF == studentDB.CPF.Number);
                studentDBCPF = studentDB.CPF.Number;
            }
        }

        [TestCleanup]
        public void Clean()
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Delete StudentDiscipline
            sql = "DELETE FROM [StudentDiscipline] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = StudentDisciplineId });

            // Delete Discipline
            _DREP.Delete(discipline.Id);

            // Delete Professor
            sql = "DELETE FROM [professor] WHERE Id = @Id";
            db.Execute(sql, param: new { professor.Id });

            // Delete User
            sql = "DELETE FROM [User] WHERE Id = @Id";
            db.Execute(sql, param: new { professor.Id });

            // Delete Enrollment
            sql = "DELETE FROM [Enrollment] WHERE Id = @Id";
            db.Execute(sql, param: new { enrollment.Id });

            // Delete Student
            _SREP.Delete(student.Id);
            var studentDB = _SREP.Get(student.Id);
            Assert.IsNull(studentDB);

            // Delete User
            sql = "DELETE FROM [User] WHERE Id = @Id";
            db.Execute(sql, param: new { student.Id });

            // Delete Course
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = course.CourseId });
        }
    }
}
