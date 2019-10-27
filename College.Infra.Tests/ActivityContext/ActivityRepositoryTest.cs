using College.Entities.ActivityContext.Entities;
using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.Infra.ActivityContext;
using College.Infra.CourseContext;
using College.Infra.DataSource;
using College.Infra.ProfessorContext;
using College.Infra.Tests.DataSource;
using College.UseCases.ActivityContext.Repositories;
using College.UseCases.CourseContext.Repositories;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace College.Infra.Tests.ActivityContext
{
    [TestClass]
    public class ActivityRepositoryTest
    {
        Discipline Discipline;
        List<Guid> Activities;
        Guid CourseId;
        Professor professor;
        string sql;
        IActivityRepository _AREP;
        IDisciplineRepository _DREP;
        [TestInitialize]
        public void Init()
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);
            _AREP = new ActivityRepository(new MSSQLDB(new DBConfiguration()));
            _DREP = new DisciplineRepository(new MSSQLDB(new DBConfiguration())); 
            Activities = new List<Guid>();
            var _PREP = new ProfessorRepository(new MSSQLDB(new DBConfiguration()));
            professor = new Professor("Thelmaryo", "Vieira Lima", "034.034.034-00", "thelmaryo@hotmail.com", "123", EDegree.Master, "123", "123");
            _PREP.Create(professor);
            
            var course = new Entities.CourseContext.Entities.Course("Sistemas de Informação");
            db.Execute("INSERT INTO Course(Id, Name) VALUES (@Id, @Name)", course);
            CourseId = course.Id;

            var discipline = new Entities.CourseContext.Entities.Discipline("Software Test", course.Id, professor.Id, 10, 1, null);
            _DREP.Create(discipline);
            Discipline = new Discipline(discipline.Id, discipline.Name);

        }

        [TestMethod]
        public void ShouldCreateAnActivity()
        {
            var activity = new Activity(Discipline, "Activity 1", DateTime.Now, 10, 50, null);
            _AREP.Create(activity);
            var activityDB = _AREP.Get(activity.Id);
            Activities.Add(activity.Id);
            Assert.IsNotNull(activityDB);
            Assert.AreEqual(activity.Description, activityDB.Description);
            Assert.AreEqual(activity.Discipline.Id, activityDB.Discipline.Id);
        }

        [TestCleanup]
        public void Clean()
        {
            foreach (var id in Activities)
                _AREP.Delete(id);
            _DREP.Delete(Discipline.Id);
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Delete Course
            db.Execute("DELETE FROM Course WHERE Id = @Id", new { Id = CourseId });

            // Delete Professor
            sql = "DELETE FROM [Professor] WHERE Id = @Id";
            db.Execute(sql, param: new { professor.Id });

            // Delete User
            sql = "DELETE FROM [User] WHERE Id = @Id";
            db.Execute(sql, param: new { professor.Id });
        }
    }
}
