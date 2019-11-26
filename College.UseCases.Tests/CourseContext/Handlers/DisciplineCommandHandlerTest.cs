using College.Entities.CourseContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.Infra.CourseContext;
using College.Infra.DataSource;
using College.Infra.ProfessorContext;
using College.UseCases.CourseContext.Handlers;
using College.UseCases.CourseContext.Inputs;
using College.UseCases.ProfessorContext.Inputs;
using College.UseCases.Services;
using College.UseCases.Shared.Result;
using College.UseCases.Tests.DataSource;
using Cryptography.EncryptContext;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace College.UseCases.Tests.CourseContext.Handlers
{
    [TestClass]
    public class DisciplineCommandHandlerTest
    {
        DisciplineCommandHandler handler;
        DisciplineInputRegister commandRegister;
        DisciplineInputUpdate commandUpdate;
        DisciplineInputDelete commandDelete;
        Discipline discipline;
        Course course;
        Entities.ProfessorContext.Entities.Professor professor;
        MSSQLDB conection;
        IEncryptor _encryptor;
        Infra.ProfessorContext.ProfessorRepository _PREPProfessorContext;
        Infra.CourseContext.ProfessorRepository _PREPCourseContext;
        DisciplineRepository _DREP;
        [TestInitialize]
        public void Init()
        {
            conection = new MSSQLDB(new DBConfiguration());
            _DREP = new DisciplineRepository(conection);
            _PREPProfessorContext = new Infra.ProfessorContext.ProfessorRepository(conection);
            _PREPCourseContext = new Infra.CourseContext.ProfessorRepository(conection);
            _encryptor = new Encryptor();
            handler = new DisciplineCommandHandler(_DREP, _PREPCourseContext);

            var db = conection.GetCon();

            var cpf = "357.034.413-40";
            string password = cpf.Replace("-", "").Replace(".", "");

            password = _encryptor.Encrypt(password, out string salt);

            professor = new Entities.ProfessorContext.Entities.Professor("Lívia", "Emanuelly Elisa", cpf, "lliviaemanuell@quarttus.com.br", "(21) 2682-8370", EDegree.Master, password, salt);

            _PREPProfessorContext.Create(professor);

            // Create Course
            course = new Course(Guid.NewGuid());
            var sql = "INSERT INTO [Course] ([Id], [Name]) VALUES (@Id, 'LTP6')";
            db.Execute(sql, param: new { Id = course.Id});

            commandRegister = new DisciplineInputRegister()
            {
                Name = "Filosofia",
                CourseId = course.Id,
                ProfessorId = professor.Id,
                WeeklyWorkload = 1,
                Period = 1
            };
            discipline = new Discipline("Filosofia", course, new Professor(professor.Id), 1, 1, 0);

            _DREP.Create(discipline);

            commandUpdate = new DisciplineInputUpdate()
            {
                DisciplineId = discipline.Id,
                Name = "Matematica",
                CourseId = course.Id,
                ProfessorId = professor.Id,
                WeeklyWorkload = 2,
                Period = 2
            };

            commandDelete = new DisciplineInputDelete()
            {
                DisciplineId = discipline.Id
            };

        }

        [TestMethod]
        public void ShouldCreateDiciplineWithSuccess()
        {
            StandardResult result = (StandardResult)handler.Handle(commandRegister);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Success"));
        }

        [TestMethod]
        public void ShouldUpdateDisciplineWithSuccess()
        {
            StandardResult result = (StandardResult)handler.Handle(commandUpdate);
            var disciplineDB = _DREP.Get(discipline.Id);
            Assert.AreEqual(disciplineDB.Id, discipline.Id);
        
        }

        [TestMethod]
        public void ShouldDeleteDisciplineWithSuccess()
        {
            StandardResult result = (StandardResult)handler.Handle(commandDelete);
            var disciplineDB = _DREP.Get(discipline.Id);
            Assert.AreEqual(disciplineDB, null);

        }

        [TestCleanup]
        public void Clean()
        {
            var db = conection.GetCon();

            // Delete Dicipline
            var sql = "DELETE FROM [Discipline] WHERE [CourseId] = @CourseId";
            db.Execute(sql, param: new { CourseId = commandRegister.CourseId });

            // Delete Dicipline
            sql = "DELETE FROM [Discipline] WHERE [Id] = @Id";
            db.Execute(sql, param: new { Id = discipline.Id });

            // Delete Professor
            sql = "DELETE FROM [Professor] WHERE [Email] = @Email";
            db.Execute(sql, param: new { Email = professor.Email.Address });

            // Delete Course
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = course.Id });
        }
    }
}
