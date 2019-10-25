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
        string sql;
        public DisciplineRepository(IDB db)
        {
            _db = db;
        }

        public void Create(Discipline discipline)
        {
            using (var db = _db.GetCon())
            {
                sql = "INSERT INTO Discipline (Id, Name, CourseId, ProfessorId, WeeklyWorkload, Period) VALUES (@Id, @Name, @CourseId, @ProfessorId, @WeeklyWorkload, @Period)";
                db.Execute(sql,
                    param: new
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
                sql = "DELETE FROM Discipline WHERE Id = @Id";
                db.Execute(sql, param: new { Id = id });
            }
        }

        public Discipline Get(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [Id]			  " +
                    "       ,[CourseId]		  " +
                    "       ,[ProfessorId]	  " +
                    "       ,[Name]			  " +
                    "       ,[WeeklyWorkload] " +
                    "       ,[Period]		  " +
                    "   FROM [Discipline]	  " +
                    "   WHERE Id = @Id		  ";
                var discipline = db.QuerySingleOrDefault<Discipline>(sql, param: new { Id = id });

                return discipline;
            }
        }

        public IEnumerable<Discipline> GetByCourse(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [Id]			  " +
                    "       ,[CourseId]		  " +
                    "       ,[ProfessorId]	  " +
                    "       ,[Name]			  " +
                    "       ,[WeeklyWorkload] " +
                    "       ,[Period]		  " +
                    "   FROM [Discipline]	  " +
                    "   WHERE CourseId = @CourseId";
                var disciplines = db.Query<Discipline>(sql, param: new { CourseId = id });

                return disciplines;
            }
        }

        public IEnumerable<Discipline> GetByEnrollment(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT d.* FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) INNER JOIN Enrollment e ON (s.EnrollmentId = e.Id) WHERE e.Id = @Id";
                var disciplines = db.Query<Discipline>(sql, param: new { Id = id });

                return disciplines;
            }
        }

        public IEnumerable<Discipline> GetByProfessor(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [Id]			  " +
                    "       ,[CourseId]		  " +
                    "       ,[ProfessorId]	  " +
                    "       ,[Name]			  " +
                    "       ,[WeeklyWorkload] " +
                    "       ,[Period]		  " +
                    "   FROM [Discipline]	  " +
                    "   WHERE ProfessorId = @ProfessorId";
                var disciplines = db.Query<Discipline>(sql, param: new { ProfessorId = id });

                return disciplines;
            }
        }
        public IEnumerable<Discipline> GetConcluded(Guid studentId)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT d.* FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) INNER JOIN Enrollment e ON (s.EnrollmentId = e.Id) WHERE e.StudentId = @Id AND s.[Status] = @Status";
                var disciplines = db.Query<Discipline>(sql, param: new { Id = studentId, Status = EStatusDiscipline.Pass });

                return disciplines;
            }
        }

        public IEnumerable<Discipline> List()
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [Id]			  " +
                    "       ,[CourseId]		  " +
                    "       ,[ProfessorId]	  " +
                    "       ,[Name]			  " +
                    "       ,[WeeklyWorkload] " +
                    "       ,[Period]		  " +
                    "   FROM [Discipline]	  ";
                var disciplines = db.Query<Discipline>(sql);
                return disciplines;
            }
        }

        public void Update(Discipline discipline)
        {
            using (var db = _db.GetCon())
            {
                sql = "UPDATE Discipline SET Name = @Name, ProfessorId = @ProfessorId, WeeklyWorkload = @WeeklyWorkload, Period = @Period WHERE Id = @Id";
                db.Execute(sql,
                    param: new
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
