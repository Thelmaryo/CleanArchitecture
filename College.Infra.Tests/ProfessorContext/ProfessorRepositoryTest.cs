using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.Infra.DataSource;
using College.Infra.ProfessorContext;
using College.Infra.Tests.DataSource;
using College.UseCases.ProfessorContext.Repositories;
using College.UseCases.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.Infra.Tests.ProfessorContext
{
    [TestClass]
    public class ProfessorRepositoryTest
    {
        private IProfessorRepository _PREP;
        [TestInitialize]
        public void Init()
        {
            _PREP = new ProfessorRepository(new MSSQLDB(new DBConfiguration()));
        }

        [TestMethod]
        public void ShouldCreateAProfessor()
        {
            var professor = new Professor("Thelmaryo", "Vieira Lima", "034.034.034-00", "thelmaryo@hotmail.com", "123", EDegree.Master,"123", "123");
            _PREP.Create(professor);
            var professorDB = _PREP.Get(professor.Id);
            Assert.IsNotNull(professorDB);
            Assert.AreEqual(professor.Name, professorDB.Name);
            Assert.IsTrue(professor.Active);
            Assert.AreEqual(professor.CPF.Number, professorDB.CPF.Number) ;
        }

        [TestMethod]
        public void ShouldDisableAProfessor()
        {
            var professor = new Professor("Thelmaryo", "Vieira Lima", "034.034.034-00", "thelmaryo@hotmail.com", "123", EDegree.Master, "123", "123");
            _PREP.Create(professor);
            _PREP.Disable(professor.Id);
            var professorDB = _PREP.Get(professor.Id);
            Assert.IsNotNull(professorDB);
            Assert.IsFalse(professorDB.Active);
        }

        [TestCleanup]
        public void Clean()
        {
        }
    }
}
