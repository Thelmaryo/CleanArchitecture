using College.Entities.CourseContext.Entities;
using College.Infra.DataSource;
using College.UseCases.CourseContext.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

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
                        CourseId = discipline.Course.Id,
                        ProfessorId = discipline.Professor.Id,
                        discipline.WeeklyWorkload,
                        discipline.Period
                    }) ;
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
                sql = "SELECT *, p.FirstName +' '+ p.LastName AS Name FROM Discipline " +
                " INNER JOIN Course c ON (c.Id = CourseId)  " +
                " INNER JOIN Professor p ON (p.Id = ProfessorId)" +
                "   WHERE Discipline.Id = @Id";
                return db.Query<Discipline, Course, Professor, Discipline>(sql, param: new { Id = id }, map:
                    (discipline, course, professor) => {
                        var d = new Discipline(discipline.Name, course, professor, discipline.WeeklyWorkload, discipline.Period, discipline.Id);
                        return d;
                    }, splitOn: "Id").SingleOrDefault();
            }
        }

        
        public IEnumerable<Discipline> GetByEnrollment(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT d.*, c.*, p.Id, p.FirstName +' '+ p.LastName AS Name " +
                    " FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) " +
                    " INNER JOIN Enrollment e ON (s.EnrollmentId = e.Id) " +
                    " INNER JOIN Course c ON (c.Id = CourseId)  " +
                    " INNER JOIN Professor p ON (p.Id = ProfessorId)" +
                    "WHERE e.Id = @Id";
                var disciplines = db.Query<Discipline, Course, Professor, Discipline>(sql, param: new { Id = id }, map:
                    (discipline, course, professor) => {
                        var d = new Discipline(discipline.Name, course, professor, discipline.WeeklyWorkload, discipline.Period, discipline.Id);
                        return d;
                    }, splitOn: "Id");
                return disciplines;
            }
        }

        public IEnumerable<Discipline> GetByProfessor(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT *, p.FirstName +' '+ p.LastName AS Name FROM Discipline " +
                " INNER JOIN Course c ON (c.Id = CourseId)  " +
                " INNER JOIN Professor p ON (p.Id = ProfessorId)" +
                "   WHERE ProfessorId = @ProfessorId";
                var disciplines = db.Query<Discipline, Course, Professor, Discipline>(sql, param: new { ProfessorId = id }, map:
                    (discipline, course, professor) => {
                        var d = new Discipline(discipline.Name, course, professor, discipline.WeeklyWorkload, discipline.Period, discipline.Id);
                        return d;
                    }, splitOn: "Id");
                return disciplines;
            }
        }
        public IEnumerable<Discipline> GetConcluded(Guid studentId)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT d.*, c.*, p.Id, p.FirstName +' '+ p.LastName AS Name " +
                    " FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) " +
                    " INNER JOIN Enrollment e ON (s.EnrollmentId = e.Id) " +
                    " INNER JOIN Course c ON (c.Id = CourseId)  " +
                    " INNER JOIN Professor p ON (p.Id = ProfessorId)" +
                    " WHERE e.StudentId = @Id AND s.[Status] = @Status";
                var disciplines = db.Query<Discipline, Course, Professor, Discipline>(sql, param: new { }, map:
                    (discipline, course, professor) => {
                        var d = new Discipline(discipline.Name, course, professor, discipline.WeeklyWorkload, discipline.Period, discipline.Id);
                        return d;
                    }, splitOn: "Id");
                return disciplines;
            }
        }

        public IEnumerable<Discipline> List()
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT *, p.FirstName +' '+ p.LastName AS Name FROM Discipline " +
                "INNER JOIN Course c ON (c.Id = CourseId)  " +
                "INNER JOIN Professor p ON (p.Id = ProfessorId)";
                var disciplines = db.Query<Discipline, Course, Professor, Discipline>(sql, map: 
                    (discipline, course, professor) => {
                        var d = new Discipline(discipline.Name, course, professor, discipline.WeeklyWorkload, discipline.Period, discipline.Id);
                        return d;
                }, splitOn: "Id");
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
                        ProfessorId = discipline.Professor.Id,
                        discipline.WeeklyWorkload,
                        discipline.Period,
                        discipline.Id
                    });
            }
        }
    }
}
