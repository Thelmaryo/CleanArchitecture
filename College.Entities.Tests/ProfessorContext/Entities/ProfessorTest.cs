using Microsoft.VisualStudio.TestTools.UnitTesting;
using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.UseCases.Services;
using Cryptography.EncryptContext;
using System.Linq;

namespace College.Entities.Tests.ProfessorContext.Entities
{
    [TestClass]
    public class ProfessorTest
    {
        Professor professor;
        IEncryptor _encryptor;
        [TestInitialize]
        public void Init()
        {
            _encryptor = new Encryptor();
            var cpf = "964.377.278-02";
            string password = cpf.Replace("-", "").Replace(".", "");

            password = _encryptor.Encrypt(password, out string salt);

            professor = new Professor("Abmael", "Silva", cpf, "ssarahcaroline@rami.com.br", "(71) 2846-3492", EDegree.Bachelor, password, salt);
        }
        [TestMethod]
        public void InstanceProfessorWithValuesCorrect()
        {
            Assert.IsTrue(professor.IsValid());
        }

        [TestMethod]
        public void InstanceProfessorWithValuesInCorrect()
        {
            professor = new Professor(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, EDegree.Bachelor, string.Empty, string.Empty);

            Assert.IsFalse(professor.IsValid());
            Assert.IsTrue(professor.Notifications.Any(x => x.Key == "Email" && x.Value != null));
            Assert.IsTrue(professor.Notifications.Any(x => x.Key == "CPF" && x.Value != null));
            Assert.IsTrue(professor.Notifications.Any(x => x.Key == "FirstName" && x.Value != null));
            Assert.IsTrue(professor.Notifications.Any(x => x.Key == "LastName" && x.Value != null));
            Assert.IsTrue(professor.Notifications.Any(x => x.Key == "Telefone" && x.Value != null));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}
