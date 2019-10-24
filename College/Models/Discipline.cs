using College.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace College.Models
{
    public class Discipline : IRepository
    {
        public Guid Id { get; set; }
        [Display(Name = "Disciplina")]
        public string Name { get; set; }
        [Display(Name = "Curso")]
        public Guid CourseId { get; set; }
        [Display(Name = "Professor")]
        public Guid ProfessorId { get; set; }
        [Display(Name = "Carga Horária Semanal")]
        public int WeeklyWorkload { get; set; }
        [Display(Name = "Periodo")]
        public int Period { get; set; }

        private readonly SqlConnection _db = new SqlConnection("Server=DESKTOP-23IN36H; database=College; User Id=sa; Password=123");

        public void Create()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "INSERT INTO Discipline (Id, Name, CourseId, ProfessorId, WeeklyWorkload, Period) VALUES (@Id, @Name, @CourseId, @ProfessorId, @WeeklyWorkload, @Period)";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Guid.NewGuid());
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@CourseId", CourseId);
            command.Parameters.AddWithValue("@ProfessorId", ProfessorId);
            command.Parameters.AddWithValue("@WeeklyWorkload", WeeklyWorkload);
            command.Parameters.AddWithValue("@Period", Period);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Edit()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "UPDATE Discipline SET Name = @Name, ProfessorId = @ProfessorId, WeeklyWorkload = @WeeklyWorkload, Period = @Period WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@ProfessorId", ProfessorId);
            command.Parameters.AddWithValue("@WeeklyWorkload", WeeklyWorkload);
            command.Parameters.AddWithValue("@Period", Period);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Delete()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "DELETE FROM Discipline WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public IEnumerable<Discipline> List()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Disciplines = new List<Discipline>();
            var sql = "SELECT * FROM Discipline";
            SqlCommand command = new SqlCommand(sql, _db);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var discipline = new Discipline
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    Name = dataRow["Name"].ToString(),
                    Period = Convert.ToInt32(dataRow["Period"]),
                    CourseId = Guid.Parse(dataRow["CourseId"].ToString()),
                    ProfessorId = Guid.Parse(dataRow["ProfessorId"].ToString()),
                    WeeklyWorkload = Convert.ToInt32(dataRow["WeeklyWorkload"])
                };
                Disciplines.Add(discipline);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Disciplines;
        }

        public void Get(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM Discipline WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
                Name = dataTable.Rows[0]["Name"].ToString();
                Period = Convert.ToInt32(dataTable.Rows[0]["Period"]);
                CourseId = Guid.Parse(dataTable.Rows[0]["CourseId"].ToString());
                ProfessorId = Guid.Parse(dataTable.Rows[0]["ProfessorId"].ToString());
                WeeklyWorkload = Convert.ToInt32(dataTable.Rows[0]["WeeklyWorkload"]);
            }

            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public IEnumerable<Discipline> GetByEnrollment(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Disciplines = new List<Discipline>();
            var sql = "SELECT d.* FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) INNER JOIN Enrollment e ON (s.EnrollmentId = e.Id) WHERE e.Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var discipline = new Discipline
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    Name = dataRow["Name"].ToString(),
                    Period = Convert.ToInt32(dataRow["Period"]),
                    CourseId = Guid.Parse(dataRow["CourseId"].ToString()),
                    ProfessorId = Guid.Parse(dataRow["ProfessorId"].ToString()),
                    WeeklyWorkload = Convert.ToInt32(dataRow["WeeklyWorkload"])
                };
                Disciplines.Add(discipline);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Disciplines;
        }
        public IEnumerable<Discipline> GetConcluded(Guid studentId)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Disciplines = new List<Discipline>();
            var sql = "SELECT d.* FROM Discipline d INNER JOIN StudentDiscipline s ON (s.DisciplineId = d.Id) INNER JOIN Enrollment e ON (s.EnrollmentId = e.Id) WHERE e.Id = @Id AND s.[Status] = @Status";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", studentId);
            command.Parameters.AddWithValue("@Status", EStatusDiscipline.Pass);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var discipline = new Discipline
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    Name = dataRow["Name"].ToString(),
                    Period = Convert.ToInt32(dataRow["Period"]),
                    CourseId = Guid.Parse(dataRow["CourseId"].ToString()),
                    ProfessorId = Guid.Parse(dataRow["ProfessorId"].ToString()),
                    WeeklyWorkload = Convert.ToInt32(dataRow["WeeklyWorkload"])
                };
                Disciplines.Add(discipline);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Disciplines;
        }

        public IEnumerable<Discipline> GetByCourse(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Disciplines = new List<Discipline>();
            var sql = "SELECT * FROM Discipline WHERE CourseId = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var discipline = new Discipline
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    Name = dataRow["Name"].ToString(),
                    Period = Convert.ToInt32(dataRow["Period"]),
                    CourseId = Guid.Parse(dataRow["CourseId"].ToString()),
                    ProfessorId = Guid.Parse(dataRow["ProfessorId"].ToString()),
                    WeeklyWorkload = Convert.ToInt32(dataRow["WeeklyWorkload"])
                };
                Disciplines.Add(discipline);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Disciplines;
        }

        public IEnumerable<Discipline> GetByProfessor(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Disciplines = new List<Discipline>();
            var sql = "SELECT * FROM Discipline WHERE ProfessorId = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var discipline = new Discipline
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    Name = dataRow["Name"].ToString(),
                    Period = Convert.ToInt32(dataRow["Period"]),
                    CourseId = Guid.Parse(dataRow["CourseId"].ToString()),
                    ProfessorId = Guid.Parse(dataRow["ProfessorId"].ToString()),
                    WeeklyWorkload = Convert.ToInt32(dataRow["WeeklyWorkload"])
                };
                Disciplines.Add(discipline);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Disciplines;
        }
    }
}