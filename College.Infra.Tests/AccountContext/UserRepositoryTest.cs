using College.Entities.AccountContext.Entities;
using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.Infra.AccountContext;
using College.Infra.DataSource;
using College.Infra.ProfessorContext;
using College.Infra.Tests.DataSource;
using College.UseCases.AccountContext.Repositories;
using College.UseCases.ProfessorContext.Repositories;
using College.UseCases.Services;
using Cryptography.EncryptContext;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace College.Infra.Tests.AccountContext
{
    [TestClass]
    public class UserRepositoryTest
    {
        Professor professor;
        string sql;
        IProfessorRepository _PREP;
        IUserRepository _UREP;
        IEncryptor _encryptor;
        User _user;
        [TestInitialize]
        public void Init()
        {
            _PREP = new ProfessorRepository(new MSSQLDB(new DBConfiguration()));
            _encryptor = new Encryptor();
            string CPF = "034.034.034-00";
            string password = _encryptor.Encrypt(CPF.Replace("-", "").Replace(".", ""), out string salt);
            professor = new Professor("Thelmaryo", "Vieira Lima", CPF, "thelmaryoTest@hotmail.com", "2134-4567", EDegree.Master, password, salt);
            _PREP.Create(professor);

            _UREP = new UserRepository(new MSSQLDB(new DBConfiguration()));
            password = _encryptor.Encrypt(CPF.Replace("-", "").Replace(".", ""), salt);
            _user = new User("thelmaryoTest@hotmail.com", password, salt, true);
        }

        [TestMethod]
        public void ShouldCreateAndGetAUser()
        {
            var UserId = _UREP.Login(_user);
            Assert.IsNotNull(UserId);
            Assert.AreEqual(professor.Id, UserId);
            Assert.IsTrue(_user.Active);
        }

        [TestCleanup]
        public void Clean()
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Delete Professor
            sql = "DELETE FROM [Professor] WHERE Id = @Id";
            db.Execute(sql, param: new { professor.Id });

            // Delete User
            sql = "DELETE FROM [User] WHERE UserName = @UserName";
            db.Execute(sql, param: new { professor.UserName });
        }
    }
}
