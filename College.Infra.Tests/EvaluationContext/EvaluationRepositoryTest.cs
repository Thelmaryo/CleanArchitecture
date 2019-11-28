using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using College.Infra.DataSource;
using College.Infra.Tests.DataSource;
using College.Infra.StudentContext;
using College.UseCases.StudentContext.Repositories;
using College.Entities.EvaluationContext.Entities;
using College.Entities.CourseContext.Entities;
using Cryptography.EncryptContext;
using System;
using Dapper;
using College.UseCases.CourseContext.Repositories;
using College.Infra.CourseContext;
using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.UseCases.ProfessorContext.Repositories;
using College.Infra.ProfessorContext;

namespace College.Infra.Tests.EvaluationContext
{
    [TestClass]
    public class EvaluationRepositoryTest
    {
        Entities.ActivityContext.Entities.Activity activityAC;
        Entities.StudentContext.Entities.Course course;
        Entities.ActivityContext.Entities.Discipline disciplineAC;
        Entities.StudentContext.Entities.Student student;
        Entities.StudentContext.Entities.Student studentEdit;
        Student _student;
        Activity activityEC;
        Entities.CourseContext.Entities.Discipline disciplineCC;
        Entities.ProfessorContext.Entities.Professor professor;
        string sql;
        UseCases.EvaluationContext.Repositories.IActivityRepository _AREPEC;
        UseCases.ActivityContext.Repositories.IActivityRepository _AREPAC;
        IDisciplineRepository _DREP;
        IStudentRepository _SREP;
        UseCases.ProfessorContext.Repositories.IProfessorRepository _PREP;

        [TestInitialize]
        public void Init()
        {
            _AREPEC = new Infra.EvaluationContext.ActivityRepository(new MSSQLDB(new DBConfiguration()));
            _AREPAC = new Infra.ActivityContext.ActivityRepository(new MSSQLDB(new DBConfiguration()));
            _DREP = new DisciplineRepository(new MSSQLDB(new DBConfiguration()));
            _PREP = new Infra.ProfessorContext.ProfessorRepository(new MSSQLDB(new DBConfiguration()));
            _SREP = new StudentRepository(new MSSQLDB(new DBConfiguration()));
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Create Course
            course = new Entities.StudentContext.Entities.Course(Guid.NewGuid());
            sql = "INSERT INTO [Course] ([Id], [Name]) VALUES (@Id, 'LTP')";
            db.Execute(sql, param: new { Id = course.CourseId });

            // Create Professor
            professor = new Entities.ProfessorContext.Entities.Professor("Thelmaryo", "Vieira Lima", "034.034.034-00", "thelmaryoTest@hotmail.com", "123", EDegree.Master, "123", "123");
            _PREP.Create(professor);

            // Create Discipline
            disciplineCC = new Entities.CourseContext.Entities.Discipline("Psicologia", new Entities.CourseContext.Entities.Course(course.CourseId), new Entities.CourseContext.Entities.Professor(professor.Id), 20, 1,0);
            _DREP.Create(disciplineCC);

            disciplineAC = new Entities.ActivityContext.Entities.Discipline(disciplineCC.Id, "Psicologia");

            // Create Student
            string CPF = "117.400.002-34";
            string password = new Encryptor().Encrypt(CPF.Replace("-", "").Replace(".", ""), out string salt);
            student = new Entities.StudentContext.Entities.Student(course, DateTime.Now, "Abmael", "Araujo", CPF, "abmaelTest@gmail.com", "1234-1234", "F", "França", "AraguainaSul", "Norte", password, salt);
            _SREP.Create(student);

            // Create Student
            string CPFEdit = "344.245.132-97";
            string passwordEdit = new Encryptor().Encrypt(CPF.Replace("-", "").Replace(".", ""), out string saltEdit);
            studentEdit = new Entities.StudentContext.Entities.Student(course, DateTime.Now, "Abmael", "Araujo", CPFEdit, "abmaelTest@gmail.com", "1234-1234", "F", "França", "AraguainaSul", "Norte", passwordEdit, saltEdit);
            _SREP.Create(studentEdit);

            // Create Activity
            activityAC = new Entities.ActivityContext.Entities.Activity(disciplineAC, "AtividadeTest", DateTime.Now, 10, 10, 15, Guid.NewGuid());
            _AREPAC.Create(activityAC);

            // Create Activity
            _student = new Student(student.Id);
            activityEC = new Activity(activityAC.Id, _student, "AtividadeTest", DateTime.Now, 8, 10);
            _AREPEC.Create(activityEC);
        }
        [TestMethod]
        public void ShouldCreateAndGetByStudentAProfessor()
        {
            var activityDB = _AREPEC.GetByStudent(student.Id, activityEC.Id);

            Assert.IsNotNull(activityDB);
            Assert.AreEqual(activityEC.Id, activityDB.Id);
        }

        [TestMethod]
        public void ShouldUpdateAProfessor()
        {
            // Porque existe esse metodo ???
            // var _student = new Student(studentEdit.Id);
            //activityEC.UpdateStudent(_student);
            var activityECEdit = new Activity(activityEC.Id, activityEC.Student, activityEC.Description, DateTime.Now, 6, activityEC.Value);

            _AREPEC.Update(activityECEdit);
            var activityDB = _AREPEC.GetByStudent(student.Id, activityEC.Id);

            Assert.IsNotNull(activityDB);
            Assert.AreEqual(activityEC.Id, activityDB.Id);
            Assert.AreNotEqual(activityEC.Grade, activityDB.Grade);
            Assert.AreEqual(activityECEdit.Grade, activityDB.Grade);
        }
        [TestCleanup]
        public void Clean()
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Delete StudentActivity
            sql = "DELETE FROM [StudentActivity] WHERE ActivityId = @Id";
            db.Execute(sql, param: new { activityAC.Id });

            // Delete Activity
            sql = "DELETE FROM [Activity] WHERE Id = @Id";
            db.Execute(sql, param: new { activityEC.Id });

            // Delete Discipline
            _DREP.Delete(disciplineCC.Id);

            // Delete Professor
            sql = "DELETE FROM [professor] WHERE Id = @Id";
            db.Execute(sql, param: new { professor.Id });

            // Delete Student
            _SREP.Delete(student.Id);

            // Delete User
            sql = "DELETE FROM [User] WHERE Id = @Id";
            db.Execute(sql, param: new { student.Id });

            // Delete Student
            _SREP.Delete(studentEdit.Id);

            // Delete User
            sql = "DELETE FROM [User] WHERE Id = @Id";
            db.Execute(sql, param: new { studentEdit.Id });

            // Delete Course
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = course.CourseId });
        }
    }
}
