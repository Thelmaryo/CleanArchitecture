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
            sql = "INSERT INTO Activity (Id, DisciplineId, Description, Value, [Date]) VALUES (@Id, @DisciplineId, @Description, @Value, @Date)";
            using var db = _db.GetCon();
            db.Execute(sql, new
            {
                activity.Id,
                DisciplineId = activity.Discipline.Id,
                activity.Description,
                activity.Value,
                activity.Date
            });
        }

        public void Delete(Guid id)
        {
            sql = "DELETE FROM Activity WHERE Id = @Id";
            using var db = _db.GetCon();
            db.Execute(sql, new { Id = id });
        }

        public void Update(Activity activity)
        {
            sql = "UPDATE Activity SET Description = @Description, Value = @Value, [Date] = @Date WHERE Id = @Id";
            using var db = _db.GetCon();
            db.Execute(sql, new
            {
                activity.Id,
                activity.Description,
                activity.Value,
                activity.Date
            });
        }

        public Activity Get(Guid id)
        {
            sql = "SELECT * FROM Activity a INNER JOIN Discipline d ON (d.Id = a.DisciplineId) WHERE a.Id = @Id";
            using var db = _db.GetCon();
            return db.Query<Activity, Discipline, Activity>(sql, param: new { Id = id }, 
                map: (activity, discipline) => {
                    activity.UpdateDiscipline(discipline);
                    return activity;
                }, splitOn: "Id").SingleOrDefault();
        }

        public IEnumerable<Activity> GetByDiscipline(Guid id, Semester semester)
        {
            sql = "SELECT * FROM Activity a INNER JOIN Discipline d ON (d.Id = a.DisciplineId) WHERE a.DisciplineId = @Id AND a.[Date] BETWEEN @Begin AND @End";
            using var db = _db.GetCon();
            return db.Query<Activity, Discipline, Activity>(sql, 
                param: new { 
                    Id = id,
                    semester.Begin,
                    semester.End
                },
                map: (activity, discipline) => {
                    activity.UpdateDiscipline(discipline);
                    return activity;
                }, splitOn: "Id");
        }
    }
}
