using College.Entities.StudentContext.Entities;
using College.Infra.DataSource;
using College.Infra.StudentContext;
using College.UseCases.Services;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Queries;
using College.UseCases.Tests.DataSource;
using Cryptography.EncryptContext;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace College.UseCases.Tests.StudentContext.Queries
{
    [TestClass]
    public class StudentQueryHandlerTest
    {
        StudentQueryHandler handler;
        StudentInputGetById commandGetById;
        StudentInputGetByCPF commandGetByCPF;
        StudentInputList commandList;
        Student student;
        Course course;
        MSSQLDB conection;
        IEncryptor _encryptor;
        StudentRepository _SREP;
        [TestInitialize]
        public void Init()
        {
            conection = new MSSQLDB(new DBConfiguration());
            _SREP = new StudentRepository(conection);
            _encryptor = new Encryptor();
            handler = new StudentQueryHandler(_SREP);

            var db = conection.GetCon();

            // Create Course
            course = new Course(Guid.NewGuid(), "LTP5");
            var sql = "INSERT INTO [Course] ([Id], [Name]) VALUES (@Id, @Name)";
            db.Execute(sql, param: new { Id = course.CourseId, Name = course.Name });

            var cpf = "964.377.278-02";
            string password = cpf.Replace("-", "").Replace(".", "");

            password = _encryptor.Encrypt(password, out string salt);

            student = new Student(course, DateTime.Now, "Abmael", "Araujo", cpf, "carolinaalinebarros@tkk.com.br", "(86) 2802-4826", "M", "Brasil", "Araguaina", "Centro", password, salt);
            _SREP.Create(student);

            commandGetById = new StudentInputGetById()
            {
                StudentId = student.Id
            };

            commandList = new StudentInputList();

            commandGetByCPF = new StudentInputGetByCPF()
            {
                StudentCPF = student.CPF.Number
            };
        }

        [TestMethod]
        public void ShouldGetByIdStudentWithSuccess()
        {
            var result = handler.Handle(commandGetById);
            Assert.IsTrue(result.Student.Id == student.Id);
            Assert.IsTrue(result.Student.CPF.Number == student.CPF.Number);
        }

        [TestMethod]
        public void ShouldListStudentWithSuccess()
        {
            var result = handler.Handle(commandList);
            var _student = result.Students.Single(x => x.CPF.Number == student.CPF.Number);
            Assert.IsTrue(_student.Id == student.Id);

        }

        [TestMethod]
        public void ShouldGetByCPFStudentWithSuccess()
        {
            var result = handler.Handle(commandGetByCPF);
            Assert.IsTrue(result.Student.Id == student.Id);
            Assert.IsTrue(result.Student.CPF.Number == student.CPF.Number);
        }

        [TestCleanup]
        public void Clean()
        {
            var db = conection.GetCon();

            // Delete Course
            var sql = "DELETE FROM [Student] WHERE [Email] = @Email";
            db.Execute(sql, param: new { Email = student.Email.Address });

            // Delete Course
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = course.CourseId });
        }
    }
}
