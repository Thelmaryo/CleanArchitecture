using College.Entities.EnrollmentContext.Entities;
using College.Entities.Shared;
using College.Infra.DataSource;
using College.UseCases.EnrollmentContext.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.Infra.EnrollmentContext
{
    public class DisciplineRepository : IDisciplineRepository
    {
        IDB _db;
        string sql;
        public DisciplineRepository(IDB db)
        {
            _db = db;
        }
        public IEnumerable<Discipline> GetByCourse(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT Id, Name FROM Discipline WHERE CourseId = @CourseId";
                return db.Query<Discipline>(sql, new { CourseId = id });
            }
        }


        public IEnumerable<Discipline> GetConcluded(Guid studentId)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT d.Id, d.Name FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) " +
                    " INNER JOIN Enrollment e ON (s.EnrollmentId = e.Id) WHERE e.StudentId = @Id AND s.[Status] = @Status";
                return db.Query<Discipline>(sql, new { Id = studentId, Status = EStatusDiscipline.Pass });
            }
        }
    }
}
