using College.Entities.CourseContext.Entities;
using College.Infra.DataSource;
using College.Infra.EnrollmentContext;
using College.Infra.Tests.DataSource;
using College.UseCases.CourseContext.Repositories;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;

namespace College.Infra.Tests.CourseContext
{
    [TestClass]
    public class CourseRepositoryTest
    {
        string sql;
        Course course1;
        Course course2;
        Course course3;
        ICourseRepository _CREP;
        [TestInitialize]
        public void Init()
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);
            _CREP = new CourseRepository(new MSSQLDB(new DBConfiguration()));
            // Create Course
            course1 = new Course("Matematica");
            sql = "INSERT INTO [Course] ([Id], [Name]) VALUES (@Id, @Name)";
            db.Execute(sql, param: new { Id = course1.Id, Name = course1.Name });
            course2 = new Course("Fisica");
            db.Execute(sql, param: new { Id = course2.Id, Name = course2.Name });
            course3 = new Course("Quimica");
            db.Execute(sql, param: new { Id = course3.Id, Name = course3.Name });
        }
        [TestMethod]
        public void ShouldListCourse()
        {
            var courseDB = _CREP.List();
            Assert.IsNotNull(courseDB);

            foreach (var course in courseDB)
            {
                if (course.Id == course1.Id)
                {
                    Assert.AreEqual(course1.Name, course.Name);
                    Assert.AreEqual(course1.Id, course.Id);
                }
                if (course.Id == course2.Id)
                {
                    Assert.AreEqual(course2.Name, course.Name);
                    Assert.AreEqual(course2.Id, course.Id);
                }
                if (course.Id == course3.Id)
                {
                    Assert.AreEqual(course3.Name, course.Name);
                    Assert.AreEqual(course3.Id, course.Id);
                }
            }
        }

        [TestCleanup]
        public void Clean()
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Delete Course1
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = course1.Id });

            // Delete Course2
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = course2.Id });

            // Delete Course3
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = course3.Id });
        }
    }
}
