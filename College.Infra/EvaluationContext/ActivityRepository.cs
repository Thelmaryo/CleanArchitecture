using College.Entities.EvaluationContext.Entities;
using College.Infra.DataSource;
using College.UseCases.EvaluationContext.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
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
            using (var db = _db.GetCon())
            {
                sql = "INSERT INTO StudentActivity (Id, StudentId, ActivityId, Grade) VALUES (@Id, @StudentId, @ActivityId, @Grade)";
                db.Execute(sql, new
                {
                    Id = Guid.NewGuid(),
                    ActivityId = activity.Id,
                    StudentId = activity.Student.Id,
                    activity.Grade
                });
            }
        }

        public IEnumerable<Activity> GetByDiscipline(Guid studentId, Guid disciplineId)
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [ActivityId] as Id, 										  " +
                " [Grade], 	[Description],														  " +
                " [Value], [Date],															  " +
                " [StudentId] as Id, s.FirstName + ' ' + s.LastName AS Name			" +
                " FROM StudentActivity st INNER JOIN Student s on s.Id = st.StudentId " +
                " INNER JOIN Activity a on a.Id = st.ActivityId 					  " +
                " WHERE a.DisciplineId = @DisciplineId AND s.Id = @StudentId			  ";
                var activity = db.Query<Activity, Student, Activity>(sql,
                    param: new { DisciplineId = disciplineId, StudentId = studentId },
                    map: (_activity, student) =>
                    {
                        _activity = new Activity(_activity.Id, new Student(student.Id, student.Name), _activity.Description, _activity.Date, _activity.Grade, _activity.Value);
                        return _activity;
                    }, splitOn: "Id, Id");

                return activity;
            }
        }

        public Activity GetByStudent(Guid studentId, Guid activityId)
        {
            using (var db = _db.GetCon())
            {
                sql = " SELECT [ActivityId] as Id, 										  " +
                " [Grade], [Description],															  " +
                " [Value], 	[Date],														  " +
                " [StudentId] as Id, s.FirstName + ' ' + s.LastName AS Name			" +
                " FROM StudentActivity st INNER JOIN Student s on s.Id = st.StudentId " +
                " INNER JOIN Activity a on a.Id = st.ActivityId 					  " +
                " WHERE st.ActivityId = @ActivityId AND s.Id = @StudentId			  ";
                var activity = db.Query<Activity, Student, Activity>(sql,
                    param: new { ActivityId = activityId, StudentId = studentId },
                    map: (_activity, student) =>
                    {
                        _activity = new Activity(_activity.Id, new Student(student.Id, student.Name), _activity.Description, _activity.Date, _activity.Grade, _activity.Value);
                        return _activity;
                    }, splitOn: "Id, Id");

                return activity.SingleOrDefault();
            }
        }

        public void Update(Activity activity)
        {
            using (var db = _db.GetCon())
            {
                sql = "UPDATE StudentActivity SET Grade = @Grade WHERE StudentId = @StudentId AND ActivityId = @ActivityId";
                db.Execute(sql, new
                {
                    ActivityId = activity.Id,
                    StudentId = activity.Student.Id,
                    activity.Grade
                });
            }
        }
    }
}
