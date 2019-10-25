using System;
using System.Collections.Generic;
using System.Linq;
using College.Entities.EnrollmentContext.Entities;
using College.Entities.EnrollmentContext.Enumerators;
using College.Infra.DataSource;
using College.UseCases.EnrollmentContext.Repositories;
using Dapper;

namespace College.Infra.EnrollmentContext
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        IDB _db;
        public EnrollmentRepository(IDB db)
        {
            _db = db;
        }
        public void Cancel(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = "UPDATE Enrollment SET [Status] = @Status WHERE Id = @Id";
                db.Execute(sql, new { Id = id, Status = EStatusEnrollment.Canceled });
            }
        }

        public void Confirm(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = "UPDATE Enrollment SET [Status] = @Status WHERE Id = @Id";
                db.Execute(sql, new { Id = id, Status = EStatusEnrollment.Confirmed });
            }
        }

        public void Create(Enrollment enrollment)
        {
            using (var db = _db.GetCon())
            {
                var sql = "INSERT INTO Enrollment (Id, StudentId, [Begin], [End], [Status]) VALUES (@Id, @StudentId, @Begin, @End, @Status)";
                db.Execute(sql,
                    new{
                        enrollment.Id,
                        StudentId = enrollment.Student.Id,
                        enrollment.Begin,
                        enrollment.End,
                        enrollment.Status
                    });
            }
        }

        public Enrollment Get(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		  " +
                        "       ,[Begin]	  " +
                        "       ,[End]		  " +
                        "       ,[Status]	  " +
                        "       ,[StudentId]  " +
                        "   FROM [Enrollment] " +
                        "   WHERE Id = @Id	  ";
                var enrollment = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                    map: (enrollment, eStatusEnrollment, student) =>
                    {
                        eStatusEnrollment = (EStatusEnrollment)eStatusEnrollment;
                        student = new Student(enrollment.Student.Id);
                        enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, eStatusEnrollment);

                        return enrollment;
                    }, new { Id = id },
                splitOn: "Id, Status, Enrollment");

                return enrollment.FirstOrDefault();
            }
        }

        public IEnumerable<Enrollment> GetByStudent(Guid studentId)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		  " +
                        "       ,[Begin]	  " +
                        "       ,[End]		  " +
                        "       ,[Status]	  " +
                        "       ,[StudentId]  " +
                        "   FROM [Enrollment] " +
                        "   WHERE studentId = @studentId";
                var enrollment = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                    map: (enrollment, eStatusEnrollment, student) =>
                    {
                        eStatusEnrollment = (EStatusEnrollment)eStatusEnrollment;
                        student = new Student(enrollment.Student.Id);
                        enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, eStatusEnrollment);

                        return enrollment;
                    }, new { studentId = studentId },
                splitOn: "Id, Status, Enrollment");

                return enrollment;
            }
        }

        public Enrollment GetCurrent(Guid studentId)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		  " +
                        "       ,[Begin]	  " +
                        "       ,[End]		  " +
                        "       ,[Status]	  " +
                        "       ,[StudentId]  " +
                        "   FROM [Enrollment] " +
                        "   WHERE studentId = @studentId";
                var enrollment = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                    map: (enrollment, eStatusEnrollment, student) =>
                    {
                        eStatusEnrollment = (EStatusEnrollment)eStatusEnrollment;
                        student = new Student(enrollment.Student.Id);
                        enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, eStatusEnrollment);

                        return enrollment;
                    }, new { studentId = studentId },
                splitOn: "Id, Status, Enrollment");

                return enrollment.FirstOrDefault();
            }
        }

        public IEnumerable<Enrollment> GetPreEnrollments()
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		  " +
                        "       ,[Begin]	  " +
                        "       ,[End]		  " +
                        "       ,[Status]	  " +
                        "       ,[StudentId]  " +
                        "   FROM [Enrollment] " +
                        "   WHERE [Status] = @Status";
                var enrollment = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                    map: (enrollment, eStatusEnrollment, student) =>
                    {
                        eStatusEnrollment = (EStatusEnrollment)eStatusEnrollment;
                        student = new Student(enrollment.Student.Id);
                        enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, eStatusEnrollment);

                        return enrollment;
                    }, new { Status = EStatusEnrollment.PreEnrollment },
                splitOn: "Id, Status, Enrollment");

                return enrollment;
            }
        }
    }
}
