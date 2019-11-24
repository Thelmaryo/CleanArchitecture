using College.Entities.StudentContext.Entities;
using College.UseCases.Services;
using Cryptography.EncryptContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace College.Entities.Tests.StudentContext.Entities
{
    [TestClass]
    public class StudentTest
    {
        Student student;
        Course course;
        IEncryptor _encryptor;
        [TestInitialize]
        public void Init()
        {
            _encryptor = new Encryptor();
            var cpf = "964.377.278-02";
            string password = cpf.Replace("-", "").Replace(".", "");

            password = _encryptor.Encrypt(password, out string salt);

            course = new Course(Guid.NewGuid(), "LTP5");
            student = new Student(course, DateTime.Now, "Abmael", "Araujo", cpf, "carolinaalinebarros@tkk.com.br", "(86) 2802-4826", "M", "Brasil", "Araguaina", "Centro", password, salt);
        }
        [TestMethod]
        public void InstanceStudentWithValuesCorrect()
        {
            var asdf = student;
            Assert.IsTrue(student.IsValid());
        }

        [TestMethod]
        public void InstanceStudentWithValuesInCorrect()
        {
            student = new Student(course, DateTime.Now, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            Assert.IsFalse(student.IsValid());
            Assert.IsTrue(student.Notifications.Any(x => x.Key == "Email" && x.Value != null));
            Assert.IsTrue(student.Notifications.Any(x => x.Key == "CPF" && x.Value != null));
            Assert.IsTrue(student.Notifications.Any(x => x.Key == "FirstName" && x.Value != null));
            Assert.IsTrue(student.Notifications.Any(x => x.Key == "LastName" && x.Value != null));
            Assert.IsTrue(student.Notifications.Any(x => x.Key == "Telefone" && x.Value != null));
            Assert.IsTrue(student.Notifications.Any(x => x.Key == "City" && x.Value != null));
            Assert.IsTrue(student.Notifications.Any(x => x.Key == "Country" && x.Value != null));
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}
