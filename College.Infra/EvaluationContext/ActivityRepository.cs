using College.Entities.EvaluationContext.Entities;
using College.Infra.DataSource;
using College.UseCases.EvaluationContext.Repositories;
using Dapper;
using System;
using System.Linq;

namespace College.Infra.EvaluationContext
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
            sql = "INSERT INTO StudentActivity (Id, StudentId, ActivityId, Grade) VALUES (@Id, @StudentId, @ActivityId, @Grade)";
            using var db = _db.GetCon();
            db.Execute(sql, new { 
                Id = Guid.NewGuid(),
                ActivityId = activity.Id,
                StudentId = activity.Student.Id,
                activity.Grade
            });
        }

        public Activity GetByStudent(Guid studentId, Guid activityId)
        {
            sql = "SELECT * FROM StudentActivity sa INNER JOIN Student s ON (s.Id = sa.StudentId) WHERE sa.StudentId = @StudentId AND sa.ActivityId = @ActivityId";
            using var db = _db.GetCon();
            return db.Query<Activity, Student, Activity>(sql,
                param: new
                {
                    ActivityId = activityId,
                    StudentId = studentId
                },
                map: (activity, student) =>
                {
                    activity.UpdateStudent(student);
                    return activity;
                }, splitOn: "Id").SingleOrDefault();
        }

        public void Update(Activity activity)
        {
            sql = "UPDATE StudentActivity SET Grade = @Grade WHERE StudentId = @StudentId AND ActivityId = @ActivityId";
            using var db = _db.GetCon();
            db.Execute(sql, new
            {
                ActivityId = activity.Id,
                StudentId = activity.Student.Id,
                activity.Grade
            });
        }
    }
}
