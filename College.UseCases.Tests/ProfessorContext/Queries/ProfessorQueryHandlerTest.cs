using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.Infra.DataSource;
using College.Infra.ProfessorContext;
using College.UseCases.ProfessorContext.Handlers;
using College.UseCases.ProfessorContext.Inputs;
using College.UseCases.ProfessorContext.Queries;
using College.UseCases.Services;
using College.UseCases.Shared.Result;
using College.UseCases.Tests.DataSource;
using Cryptography.EncryptContext;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace College.UseCases.Tests.ProfessorContext.Queries
{
    [TestClass]
    public class ProfessorQueryHandlerTest
    {
        ProfessorQueryHandler handler;
        ProfessorInputGet commandGet;
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
            handler = new ProfessorQueryHandler(_PREP);

            var db = conection.GetCon();

            var cpf = "357.034.413-40";
            string password = cpf.Replace("-", "").Replace(".", "");

            password = _encryptor.Encrypt(password, out string salt);

            professor = new Professor("Lívia", "Emanuelly Elisa", cpf, "lliviaemanuell@quarttus.com.br", "(21) 2682-8370", EDegree.Master, password, salt);
            _PREP.Create(professor);

            commandGet = new ProfessorInputGet() 
            {
                ProfessorId = professor.Id
            };
        }

        [TestMethod]
        public void ShouldCreateProfessorWithSuccess()
        {
            var result = handler.Handle(commandGet);
            Assert.IsTrue(result.Professor.Id == professor.Id);
        }

        [TestMethod]
        public void ShouldUpdateProfessorWithSuccess()
        {
            var result = handler.Handle();
            var _professor = result.Professors.Single(x => x.CPF.Number == professor.CPF.Number);
            Assert.IsTrue(_professor.Id == professor.Id);

        }

        [TestCleanup]
        public void Clean()
        {
            var db = conection.GetCon();

            // Delete Course
            var sql = "DELETE FROM [Professor] WHERE [Email] = @Email";
            db.Execute(sql, param: new { Email = professor.Email.Address });

        }
    }
}
