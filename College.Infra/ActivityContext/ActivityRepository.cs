using College.Entities.ActivityContext.Entities;
using College.Infra.DataSource;
using College.UseCases.ActivityContext.Repositories;
using College.UseCases.Shared;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace College.Infra.ActivityContext
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IDB _db;
        private string sql;

        public ActivityRepository(IDB db)
        {
            _db = db;
        }

        public void Create(Activity activity)
        {
            using (var db = _db.GetCon())
            {
                sql = "INSERT INTO Activity (Id, DisciplineId, Description, Value, [Date]) VALUES (@Id, @DisciplineId, @Description, @Value, @Date)";
                db.Execute(sql, new
                {
                    activity.Id,
                    DisciplineId = activity.Discipline.Id,
                    activity.Description,
                    activity.Value,
                    activity.Date
                });
            }
        }

        public void Delete(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = "DELETE FROM Activity WHERE Id = @Id";
                db.Execute(sql, new { Id = id });
            }
        }

        public void Update(Activity activity)
        {
            using (var db = _db.GetCon())
            {
                sql = "UPDATE Activity SET Description = @Description, Value = @Value, [Date] = @Date WHERE Id = @Id";
                db.Execute(sql, new
                {
                    activity.Id,
                    activity.Description,
                    activity.Value,
                    activity.Date
                });
            }
        }

        public Activity Get(Guid id)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT * FROM Activity a INNER JOIN Discipline d ON (d.Id = a.DisciplineId) WHERE a.Id = @Id";
                return db.Query<Activity, Discipline, Activity>(sql, param: new { Id = id },
                    map: (activity, discipline) =>
                    {
                        activity.UpdateDiscipline(discipline);
                        return activity;
                    }, splitOn: "Id").SingleOrDefault();
            }
        }

        public IEnumerable<Activity> GetByDiscipline(Guid id, Semester semester)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT * FROM Activity a INNER JOIN Discipline d ON (d.Id = a.DisciplineId) WHERE a.DisciplineId = @Id AND a.[Date] BETWEEN @Begin AND @End";
                return db.Query<Activity, Discipline, Activity>(sql,
                    param: new
                    {
                        Id = id,
                        semester.Begin,
                        semester.End
                    },
                    map: (activity, discipline) =>
                    {
                        activity.UpdateDiscipline(discipline);
                        return activity;
                    }, splitOn: "Id");
            }
        }
    }
}
