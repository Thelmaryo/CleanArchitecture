using System;
using System.Collections.Generic;
using College.Entities.CourseContext.Entities;
using College.Infra.DataSource;
using College.UseCases.CourseContext.Repositories;
using Dapper;

namespace College.Infra.CourseContext
{
    public class ProfessorRepository : IProfessorRepository
    {
        IDB _db;
        string sql;
        public ProfessorRepository(IDB db)
        {
            _db = db;
        }

        public int GetWorkload(Guid professorId)
        {
            using (var db = _db.GetCon())
            {
                var sql = "SELECT SUM(WeeklyWorkload) AS Workload FROM Discipline WHERE ProfessorId = @Id";
                var workload = db.QuerySingleOrDefault<int>(sql, param: new { Id = professorId });
                return workload;
            }
        }

        public IEnumerable<Professor> List()
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT p.[Id], p.[Degree], p.FirstName +' '+ p.LastName	AS Name " +
                    " FROM [Professor] as p	 " +
                    " inner join [User] as u " +
                    " on (p.Id = u.Id) WHERE u.Active = 1";
                var professors = db.Query<Professor>(sql);
                return professors;
            }
        }
    }
}
