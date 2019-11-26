using College.Entities.ProfessorContext.Entities;
using College.Infra.DataSource;
using College.Infra.ProfessorContext;
using College.UseCases.Services;
using College.UseCases.Shared.Result;
using College.UseCases.ProfessorContext.Handlers;
using College.UseCases.ProfessorContext.Inputs;
using College.UseCases.Tests.DataSource;
using Cryptography.EncryptContext;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using College.Entities.ProfessorContext.Enumerators;

namespace College.UseCases.Tests.ProfessorContext.Handlers
{
    [TestClass]
    public class ProfessorCommandHandlerTest
    {
        ProfessorCommandHandler handler;
        ProfessorInputRegister commandRegister;
        ProfessorInputUpdate commandUpdate;
        ProfessorInputDelete commandDelete;
        Professor professor;
        MSSQLDB conection;
        IEncryptor _encryptor;
        ProfessorRepository _PREP;
        [TestInitialize]
        public void Init()
        {
            conection = new MSSQLDB(new DBConfiguration());
            _PREP = new ProfessorRepository(conection);
            _encryptor = new Encryptor();
            handler = new ProfessorCommandHandler(_PREP, _encryptor);

            var db = conection.GetCon();

            var cpf = "357.034.413-40";
            string password = cpf.Replace("-", "").Replace(".", "");

            password = _encryptor.Encrypt(password, out string salt);

            professor = new Professor("Lívia", "Emanuelly Elisa", cpf, "lliviaemanuell@quarttus.com.br", "(21) 2682-8370", EDegree.Master, password, salt);
            _PREP.Create(professor);

            commandRegister = new ProfessorInputRegister() 
            {
                FirstName = "Lívia",
                LastName = "Emanuelly Elisa",
                CPF = cpf,
                Email = "lliviaemanuell@quarttus.com.br",
                Phone = "(21) 2682-8370",
                Degree = EDegree.Master
            };

            commandUpdate = new ProfessorInputUpdate() 
            {
                ProfessorId = professor.Id,
                FirstName = "Lívia",
                LastName = "Emanuelly Elisa",
                Email = "lliviaemanuell@quarttus.com.br",
                Phone = "(21) 2682-8370",
                Degree = EDegree.Master
            };
        }

        [TestMethod]
        public void ShouldCreateProfessorWithSuccess()
        {
            StandardResult result = (StandardResult)handler.Handle(commandRegister);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Success"));
        }

        [TestMethod]
        public void ShouldCreateProfessorWithError()
        {
            commandRegister = new ProfessorInputRegister()
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                CPF = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Degree = EDegree.Master
            };

            StandardResult result = (StandardResult)handler.Handle(commandRegister);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Email" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "CPF" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "FirstName" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "LastName" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Telefone" && x.Value != null));
        }

        [TestMethod]
        public void ShouldUpdateProfessorWithSuccess()
        {
            StandardResult result = (StandardResult)handler.Handle(commandUpdate);
            var professorDB = _PREP.Get(professor.Id);
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Success"));
            Assert.AreEqual(commandUpdate.ProfessorId, professorDB.Id);
            Assert.AreEqual(commandUpdate.FirstName, professorDB.FirstName);
            Assert.AreEqual(commandUpdate.LastName, professorDB.LastName);
            Assert.AreEqual(commandUpdate.Email, professorDB.Email.Address);
            Assert.AreEqual(commandUpdate.Phone, professorDB.Phone);
            Assert.AreEqual(commandUpdate.Degree, professorDB.Degree);

        }

        [TestMethod]
        public void ShouldUpdateProfessorWithError()
        {
            commandUpdate = new ProfessorInputUpdate()
            {
                ProfessorId = professor.Id,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Degree = EDegree.Master
            };
            StandardResult result = (StandardResult)handler.Handle(commandUpdate);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Email" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "FirstName" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "LastName" && x.Value != null));
            Assert.IsTrue(result.Notifications.Any(x => x.Key == "Telefone" && x.Value != null));

        }

        [TestMethod]
        public void ShouldDeleteProfessorWithSuccess()
        {
            commandDelete = new ProfessorInputDelete() { ProfessorId = professor.Id };
            StandardResult result = (StandardResult)handler.Handle(commandDelete);
            var professorDB = _PREP.Get(professor.Id);
            Assert.IsFalse(professorDB.Active);
        }

        [TestCleanup]
        public void Clean()
        {
            var db = conection.GetCon();

            // Delete Course
            var sql = "DELETE FROM [Professor] WHERE [Email] = @Email";
            db.Execute(sql, param: new { Email = commandRegister.Email });
            db.Execute(sql, param: new { Email = commandUpdate.Email });
            db.Execute(sql, param: new { Email = professor.Email.Address });

        }
    }
}
