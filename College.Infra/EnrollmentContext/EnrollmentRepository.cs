﻿using System;
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
        string sql;
        public EnrollmentRepository(IDB db)
        {
            _db = db;
        }
        public void Cancel(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = "UPDATE Enrollment SET [Status] = @Status WHERE Id = @Id";
                db.Execute(sql, param: new { Id = id, Status = EStatusEnrollment.Canceled });
            }
        }

        public void Confirm(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = "UPDATE Enrollment SET [Status] = @Status WHERE Id = @Id";
                db.Execute(sql, param: new { Id = id, Status = EStatusEnrollment.Confirmed });
            }
        }

        public void Create(Enrollment enrollment)
        {
            using (var db = _db.GetCon())
            {
                sql = "INSERT INTO Enrollment (Id, StudentId, [Begin], [End], [Status]) VALUES (@Id, @StudentId, @Begin, @End, @Status)";
                db.Execute(sql,
                    param: new
                    {
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
                sql = " SELECT [Id]				  " +
                " ,[Begin]					  " +
                " ,[End]					  " +
                " ,[Status]					  " +
                " ,[StudentId] as Id		  " +
                "   FROM [Enrollment] " +
                "   WHERE Id = @Id	  ";
                var enrollments = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                    param: new { Id = id },
                    map: (enrollment, eStatusEnrollment, student) =>
                    {
                        student = new Student(student.Id);
                        enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, eStatusEnrollment, enrollment.Id);

                        return enrollment;
                    },
                splitOn: "Id, Status, Id");

                return enrollments.SingleOrDefault();
            }
        }

        public IEnumerable<Enrollment> GetByStudent(Guid studentId)
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [Id]				  " +
                " ,[Begin]					  " +
                " ,[End]					  " +
                " ,[Status]					  " +
                " ,[StudentId] as Id		  " +
                "   FROM [Enrollment] " +
                "   WHERE studentId = @studentId " +
                 "AND [Status] = @Status";
                var enrollments = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                    param: new { studentId, Status = EStatusEnrollment.Confirmed },
                    map: (enrollment, eStatusEnrollment, student) =>
                    {
                        student = new Student(student.Id);
                        enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, eStatusEnrollment, enrollment.Id);

                        return enrollment;
                    },
                splitOn: "Id, Status, Id");

                return enrollments;
            }
        }

        public Enrollment GetCurrent(Guid studentId)
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [Id]				  " +
                " ,[Begin]					  " +
                " ,[End]					  " +
                " ,[Status]					  " +
                " ,[StudentId] as Id		  " +
                " FROM [Enrollment]			  " +
                " WHERE studentId = @studentId " +
                "AND [Status] = @Status";
                var enrollments = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                    param: new { studentId, Status = EStatusEnrollment.Confirmed },
                    map: (enrollment, eStatusEnrollment, student) =>
                    {
                        student = new Student(student.Id);
                        enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, eStatusEnrollment, enrollment.Id);

                        return enrollment;
                    },
                splitOn: "Id, Status, Id");

                return enrollments.SingleOrDefault();
            }
        }

        public IEnumerable<Enrollment> GetPreEnrollments()
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [Id]				  " +
                " ,[Begin]					  " +
                " ,[End]					  " +
                " ,[Status]					  " +
                " ,[StudentId] as Id		  " +
                "   FROM [Enrollment]         " +
                "   WHERE [Status] = @Status  ";
                var enrollments = db.Query<Enrollment, EStatusEnrollment, Student, Enrollment>(sql,
                    param: new { Status = EStatusEnrollment.PreEnrollment },
                    map: (enrollment, Status, student) =>
                    {
                        student = new Student(student.Id);
                        enrollment = new Enrollment(student, enrollment.Begin, enrollment.End, Status, enrollment.Id);

                        return enrollment;
                    },
                splitOn: "Id, Status, Id");

                return enrollments;
            }
        }
    }
}
