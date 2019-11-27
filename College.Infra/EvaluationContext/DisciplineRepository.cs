using College.Entities.EvaluationContext.Entities;
using College.Infra.DataSource;
using College.UseCases.EvaluationContext.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace College.Infra.EvaluationContext
{
    public class DisciplineRepository : IDisciplineRepository
    {
        IDB _db;
        string sql;
        public DisciplineRepository(IDB db)
        {
            _db = db;
        }
        
        public IEnumerable<Discipline> GetByEnrollment(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT d.Id, d.Name FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) WHERE s.EnrollmentId = @Id";
                return db.Query<Discipline>(sql, new { Id = id });
            }
        }
    }
}
