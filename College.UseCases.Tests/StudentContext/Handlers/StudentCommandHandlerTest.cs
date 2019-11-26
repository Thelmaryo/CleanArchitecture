using College.Entities.StudentContext.Entities;
using College.Infra.DataSource;
using College.Infra.StudentContext;
using College.UseCases.Services;
using College.UseCases.Shared.Result;
using College.UseCases.StudentContext.Handlers;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.Tests.DataSource;
using Cryptography.EncryptContext;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace College.UseCases.Tests.StudentContext.Handlers
{
    [TestClass]
    public class StudentQueriesHandlerTest
    {
        StudentCommandHandler handler;
        StudentInputRegister commandRegister;
        StudentInputUpdate commandUpdate;
        StudentInputDelete commandDelete;
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
            handler = new StudentCommandHandler(_SREP, _encryptor);

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

            commandRegister = new StudentInputRegister() 
            {
                CourseId = course.CourseId,
                Birthdate = DateTime.Now,
                FirstName = "Ester",
                LastName = "Emily Oliveira",
                CPF = "494.035.320-68",
                Email = "esteremil@velox.com.br",
                Phone = "(45) 2509-8770",
                Gender = "M",
                Country = "Brasil",
                City = "Araguaina",
                Address = "Centro"
            };

            commandUpdate = new StudentInputUpdate() 
            {
                CourseId = course.CourseId,
                Birthdate = DateTime.Now,
                FirstName = "Iago",
                LastName = "Bernardo Nunes",
                Email = "iiagobernardo@andre.com",
                Phone = "(65) 3544-9294",
                Gender = "M",
                Country = "França",
                City = "Budapeste",
                Address = "Norte",
                StudentId = student.Id
            };
        }

        [TestMethod]
        public void ShouldCreateStudentWithSuccess()
        {
            StandardResult result = (StandardResult)handler.Handle(commandRegister);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Success"));
        }

        [TestMethod]
        public void ShouldCreateStudentWithError()
        {
            commandRegister = new StudentInputRegister()
            {
                CourseId = course.CourseId,
                Birthdate = DateTime.Now,
                FirstName = string.Empty,
                LastName = string.Empty,
                CPF = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Gender = string.Empty,
                Country = string.Empty,
                City = string.Empty,
                Address = string.Empty
            };

            StandardResult result = (StandardResult)handler.Handle(commandRegister);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Email" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "CPF" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "FirstName" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "LastName" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Telefone" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "City" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Country" && x.Value != null));
        }

        [TestMethod]
        public void ShouldUpdateStudentWithSuccess()
        {
            StandardResult result = (StandardResult)handler.Handle(commandUpdate);
            var studentDB = _SREP.Get(student.Id);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Success"));
            Assert.AreEqual(commandUpdate.CourseId, studentDB.Course.CourseId);
            Assert.AreEqual(commandUpdate.Birthdate.ToString("dd/MM/yyyy"), studentDB.Birthdate.ToString("dd/MM/yyyy"));
            Assert.AreEqual(commandUpdate.FirstName, studentDB.FirstName);
            Assert.AreEqual(commandUpdate.LastName, studentDB.LastName);
            Assert.AreEqual(commandUpdate.Email, studentDB.Email.Address);
            Assert.AreEqual(commandUpdate.Phone, studentDB.Phone);
            Assert.AreEqual(commandUpdate.Gender, studentDB.Gender);
            Assert.AreEqual(commandUpdate.Country, studentDB.Country);
            Assert.AreEqual(commandUpdate.City, studentDB.City);
            Assert.AreEqual(commandUpdate.Address, studentDB.Address);
            Assert.AreEqual(commandUpdate.StudentId, studentDB.Id);

        }

        [TestMethod]
        public void ShouldUpdateStudentWithError()
        {
            commandUpdate = new StudentInputUpdate()
            {
                CourseId = course.CourseId,
                Birthdate = DateTime.Now,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Gender = string.Empty,
                Country = string.Empty,
                City = string.Empty,
                Address = string.Empty,
                StudentId = student.Id
            };
            StandardResult result = (StandardResult)handler.Handle(commandUpdate);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Email" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "FirstName" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "LastName" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Telefone" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "City" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Country" && x.Value != null));

        }

        [TestMethod]
        public void ShouldDeleteStudentWithSuccess()
        {
            commandDelete = new StudentInputDelete() { StudentId = student.Id };
            StandardResult result = (StandardResult)handler.Handle(commandDelete);
            var studentDB = _SREP.Get(student.Id);
            Assert.AreEqual(studentDB, null);
        }

        [TestCleanup]
        public void Clean()
        {
            var db = conection.GetCon();

            // Delete Course
            var sql = "DELETE FROM [Student] WHERE [Email] = @Email";
            db.Execute(sql, param: new { Email = commandRegister.Email });
            db.Execute(sql, param: new { Email = commandUpdate.Email });
            db.Execute(sql, param: new { Email = student.Email.Address });

            // Delete Course
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = course.CourseId });
        }
    }
}
