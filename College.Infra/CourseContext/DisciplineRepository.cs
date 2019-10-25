using College.Entities.CourseContext.Entities;
using College.Infra.CourseContext.Enumerators;
using College.Infra.DataSource;
using College.UseCases.CourseContext.Repositories;
using Dapper;
using System;
using System.Collections.Generic;

namespace College.Infra.CourseContext
{
    public class DisciplineRepository : IDisciplineRepository
    {
        IDB _db;

        public DisciplineRepository(IDB db)
        {
            _db = db;
        }

        public void Create(Discipline discipline)
        {
            using (var db = _db.GetCon())
            {
                var sql = "INSERT INTO Discipline (Id, Name, CourseId, ProfessorId, WeeklyWorkload, Period) VALUES (@Id, @Name, @CourseId, @ProfessorId, @WeeklyWorkload, @Period)";
                db.Execute(sql,
                    new
                    {
                        discipline.Id,
                        discipline.Name,
                        discipline.CourseId,
                        discipline.ProfessorId,
                        discipline.WeeklyWorkload,
                        discipline.Period
                    });
            }
        }

        public void Delete(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = "DELETE FROM Discipline WHERE Id = @Id";
                db.Execute(sql, new { Id = id });
            }
        }

        public Discipline Get(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]			  " +
                        "       ,[CourseId]		  " +
                        "       ,[ProfessorId]	  " +
                        "       ,[Name]			  " +
                        "       ,[WeeklyWorkload] " +
                        "       ,[Period]		  " +
                        "   FROM [Discipline]	  " +
                        "   WHERE Id = @Id		  ";
                return db.QueryFirstOrDefault<Discipline>(sql, new { Id = id });
            }
        }

        public IEnumerable<Discipline> GetByCourse(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]			  " +
                        "       ,[CourseId]		  " +
                        "       ,[ProfessorId]	  " +
                        "       ,[Name]			  " +
                        "       ,[WeeklyWorkload] " +
                        "       ,[Period]		  " +
                        "   FROM [Discipline]	  " +
                        "   WHERE CourseId = @CourseId";
                return db.Query<Discipline>(sql, new { CourseId = id });
            }
        }

        public IEnumerable<Discipline> GetByEnrollment(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = "SELECT d.* FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) INNER JOIN Enrollment e ON (s.EnrollmentId = e.Id) WHERE e.Id = @Id";
                return db.Query<Discipline>(sql, new { Id = id });
            }
        }

        public IEnumerable<Discipline> GetByProfessor(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]			  " +
                        "       ,[CourseId]		  " +
                        "       ,[ProfessorId]	  " +
                        "       ,[Name]			  " +
                        "       ,[WeeklyWorkload] " +
                        "       ,[Period]		  " +
                        "   FROM [Discipline]	  " +
                        "   WHERE ProfessorId = @ProfessorId";
                return db.Query<Discipline>(sql, new { ProfessorId = id });
            }
        }
        public IEnumerable<Discipline> GetConcluded(Guid studentId)
        {
            using (var db = _db.GetCon())
            {
                var sql = "SELECT d.* FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) INNER JOIN Enrollment e ON (s.EnrollmentId = e.Id) WHERE e.StudentId = @Id AND s.[Status] = @Status";
                return db.Query<Discipline>(sql, new { Id = studentId, Status = EStatusDiscipline.Pass });
            }
        }

        public IEnumerable<Discipline> List()
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]			  " +
                        "       ,[CourseId]		  " +
                        "       ,[ProfessorId]	  " +
                        "       ,[Name]			  " +
                        "       ,[WeeklyWorkload] " +
                        "       ,[Period]		  " +
                        "   FROM [Discipline]	  ";
                return db.Query<Discipline>(sql);
            }
        }

        public void Update(Discipline discipline)
        {
            using (var db = _db.GetCon())
            {
                var sql = "UPDATE Discipline SET Name = @Name, ProfessorId = @ProfessorId, WeeklyWorkload = @WeeklyWorkload, Period = @Period WHERE Id = @Id";
                db.Execute(sql,
                    new
                    {
                        discipline.Name,
                        discipline.ProfessorId,
                        discipline.WeeklyWorkload,
                        discipline.Period,
                        discipline.Id
                    });
            }
        }
    }
}
