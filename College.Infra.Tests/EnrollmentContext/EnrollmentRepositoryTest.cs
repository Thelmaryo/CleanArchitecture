using College.Entities.EnrollmentContext.Entities;
using College.Entities.EnrollmentContext.Enumerators;
using College.Infra.DataSource;
using College.Infra.EnrollmentContext;
using College.Infra.StudentContext;
using College.Infra.Tests.DataSource;
using College.UseCases.EnrollmentContext.Repositories;
using College.UseCases.StudentContext.Repositories;
using Cryptography.EncryptContext;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace College.Infra.Tests.EnrollmentContext
{
    [TestClass]
    public class EnrollmentRepositoryTest
    {
        Entities.StudentContext.Entities.Student student;
        Entities.StudentContext.Entities.Course course;
        Enrollment enrollment;
        string sql;
        IEnrollmentRepository _EREP;
        IStudentRepository _SREP;
        [TestInitialize]
        public void Init()
        {
            _EREP = new EnrollmentRepository(new MSSQLDB(new DBConfiguration()));
            _SREP = new StudentRepository(new MSSQLDB(new DBConfiguration()));
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Create Course
            course = new Entities.StudentContext.Entities.Course(Guid.NewGuid());
            sql = "INSERT INTO [Course] ([Id], [Name]) VALUES (@Id, 'LTP')";
            db.Execute(sql, param: new { Id = course.CourseId });

            // Create Student
            string CPF = "117.400.002-34";
            string password = new Encryptor().Encrypt(CPF.Replace("-", "").Replace(".", ""), out string salt);
            student = new Entities.StudentContext.Entities.Student(course, DateTime.Now, "Abmael", "Araujo", CPF, "abmaelTest@gmail.com", "1234-1234", "F", "França", "AraguainaSul", "Norte", password, salt);
            _SREP.Create(student);
            var _student = new Student(student.Id, student.FirstName);

            // Create Enrollment
            enrollment = new Enrollment(_student, Convert.ToDateTime("2000-01-01"), Convert.ToDateTime("2025-01-01"), EStatusEnrollment.PreEnrollment);
            _EREP.Create(enrollment);
        }
        [TestMethod]
        public void ShouldCancelAEnrollment()
        {
            _EREP.Cancel(enrollment.Id);

            var enrollmentDB = GetEnrollmentCanceled(enrollment.Student.Id);
            Assert.IsNotNull(enrollmentDB);
            Assert.AreEqual(enrollment.Id, enrollmentDB.Id);
            Assert.AreEqual(enrollment.Student.Id, enrollmentDB.Student.Id);
            Assert.AreEqual(enrollment.Begin, enrollmentDB.Begin);
            Assert.AreEqual(enrollment.End, enrollmentDB.End);
            Assert.AreEqual(EStatusEnrollment.Canceled, enrollmentDB.Status);
        }
        [TestMethod]
        public void ShouldConfirmAEnrollment()
        {
            _EREP.Confirm(enrollment.Id);

            var enrollmentDB = _EREP.GetCurrent(enrollment.Student.Id);
            Assert.IsNotNull(enrollmentDB);
            Assert.AreEqual(enrollment.Id, enrollmentDB.Id);
            Assert.AreEqual(enrollment.Student.Id, enrollmentDB.Student.Id);
            Assert.AreEqual(enrollment.Begin, enrollmentDB.Begin);
            Assert.AreEqual(enrollment.End, enrollmentDB.End);
            Assert.AreEqual(EStatusEnrollment.Confirmed, enrollmentDB.Status);
        }
        [TestMethod]
        public void ShouldGetCurrentAEnrollment()
        {
            var enrollmentDB = GetEnrollmentPreEnrollment(enrollment.Student.Id);

            Assert.IsNotNull(enrollmentDB);
            Assert.AreEqual(enrollment.Id, enrollmentDB.Id);
            Assert.AreEqual(enrollment.Student.Id, enrollmentDB.Student.Id);
            Assert.AreEqual(enrollment.Begin, enrollmentDB.Begin);
            Assert.AreEqual(enrollment.End, enrollmentDB.End);
            Assert.AreEqual(enrollment.Status, EStatusEnrollment.PreEnrollment);
        }
        [TestMethod]
        public void ShouldGetAEnrollment()
        {
            var enrollmentDB = _EREP.Get(enrollment.Id);
            Assert.IsNotNull(enrollmentDB);
            Assert.AreEqual(enrollment.Id, enrollmentDB.Id);
            Assert.AreEqual(enrollment.Begin, enrollmentDB.Begin);
            Assert.AreEqual(enrollment.End, enrollmentDB.End);
            Assert.AreEqual(enrollment.Status, EStatusEnrollment.PreEnrollment);
        }
        [TestMethod]
        public void ShouldGetByStudentAEnrollment()
        {
            var enrollmentsDB = _EREP.GetByStudent(enrollment.Student.Id);
            Assert.IsNotNull(enrollmentsDB);
            foreach (var enrollmentDB in enrollmentsDB)
            {
                Assert.AreEqual(enrollment.Status, EStatusEnrollment.PreEnrollment);
            }
        }
        [TestMethod]
        public void ShouldGetPreEnrollmentsAEnrollment()
        {
            var enrollmentsDB = _EREP.GetPreEnrollments();
            Assert.IsNotNull(enrollmentsDB);
            foreach (var enrollmentDB in enrollmentsDB)
            {
                Assert.AreEqual(enrollment.Status, EStatusEnrollment.PreEnrollment);
            }
        }

        private Enrollment GetEnrollmentCanceled(Guid studentId)
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);
            var sql = "SELECT [Id], [Begin], [End], [Status], [StudentId] as Id FROM [Enrollment] WHERE studentId = @studentId AND [Status] = @Status";
            var enrollments = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                param: new { studentId = studentId, Status = EStatusEnrollment.Canceled },
                map: (enrollment, Status, student) =>
                {
                    student = new Student(student.Id);
                    enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, Status, enrollment.Id);

                    return enrollment;
                },
            splitOn: "Id, Status, Id");

            return enrollments.SingleOrDefault();
        }

        private Enrollment GetEnrollmentPreEnrollment(Guid studentId)
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);
            var sql = "SELECT [Id], [Begin], [End], [Status], [StudentId] as Id FROM [Enrollment] WHERE studentId = @studentId AND [Status] = @Status";
            var enrollments = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                param: new { studentId = studentId, Status = EStatusEnrollment.PreEnrollment },
                map: (enrollment, Status, student) =>
                {
                    student = new Student(student.Id);
                    enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, Status, enrollment.Id);

                    return enrollment;
                },
            splitOn: "Id, Status, Id");

            return enrollments.SingleOrDefault();
        }

        [TestCleanup]
        public void Clean()
        {
            var db = new SqlConnection(new DBConfiguration().StringConnection);

            // Delete Enrollment
            sql = "DELETE FROM [Enrollment] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = enrollment.Id });

            // Delete Student
            _SREP.Delete(student.Id);

            // Delete User
            sql = "DELETE FROM [User] WHERE Id = @Id";
            db.Execute(sql, param: new { student.Id });


            // Delete Course
            sql = "DELETE FROM [Course] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = course.CourseId });
        }
    }
}
