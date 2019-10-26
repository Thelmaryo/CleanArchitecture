using College.Enumerators;
using College.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace College.Models
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        [Display(Name = "Acadêmico")]
        public Guid StudentId { get; set; }
        [Display(Name = "Inicio")]
        public DateTime Begin { get; set; }
        [Display(Name = "Término")]
        public DateTime End { get; set; }
        public EStatusEnrollment Status { get; set; }

        private readonly SqlConnection _db = new SqlConnection("Server=DESKTOP-N1T1LL1; database=College; User Id=sa; Password=123");

        public void Create(IEnumerable<Checkbox> disciplines)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "INSERT INTO Enrollment (Id, StudentId, [Begin], [End], [Status]) VALUES (@Id, @StudentId, @Begin, @End, @Status)";
            SqlCommand command = new SqlCommand(sql, _db);
            Id = Guid.NewGuid();
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@StudentId", StudentId);
            command.Parameters.AddWithValue("@Begin", Begin);
            command.Parameters.AddWithValue("@End", End);
            command.Parameters.AddWithValue("@Status", EStatusEnrollment.PreEnrollment);
            command.ExecuteNonQuery();
            foreach (var discipline in disciplines)
            {
                sql = "INSERT INTO StudentDiscipline (Id, EnrollmentId, DisciplineId, [Status]) VALUES (@Id, @EnrollmentId, @DisciplineId, @Status)";
                command = new SqlCommand(sql, _db);
                command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                command.Parameters.AddWithValue("@EnrollmentId", Id);
                command.Parameters.AddWithValue("@DisciplineId", Guid.Parse(discipline.Value));
                command.Parameters.AddWithValue("@Status", EStatusDiscipline.Enrolled);
                command.ExecuteNonQuery();
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Confirm()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "UPDATE Enrollment SET [Status] = @Status WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Status", EStatusEnrollment.Confirmed);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Cancel()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "UPDATE Enrollment SET [Status] = @Status WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Status", EStatusEnrollment.Canceled);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public IEnumerable<Enrollment> GetPreEnrollments()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Enrollments = new List<Enrollment>();
            var sql = "SELECT * FROM Enrollment WHERE [Status] = @Status";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Status", EStatusEnrollment.PreEnrollment);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var enrollment = new Enrollment
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    Begin = Convert.ToDateTime(dataRow["Begin"]),
                    End = Convert.ToDateTime(dataRow["End"]),
                    StudentId = Guid.Parse(dataRow["StudentId"].ToString()),
                    Status = (EStatusEnrollment)Convert.ToInt32(dataRow["Status"])
                };
                Enrollments.Add(enrollment);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Enrollments;
        }

        public IEnumerable<Enrollment> GetByStudent(Guid studentId)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Enrollments = new List<Enrollment>();
            var sql = "SELECT * FROM Enrollment WHERE StudentId = @StudentId";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@StudentId", studentId);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var enrollment = new Enrollment
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    Begin = Convert.ToDateTime(dataRow["Begin"]),
                    End = Convert.ToDateTime(dataRow["End"]),
                    StudentId = Guid.Parse(dataRow["StudentId"].ToString()),
                    Status = (EStatusEnrollment)Convert.ToInt32(dataRow["Status"])
                };
                Enrollments.Add(enrollment);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Enrollments;
        }


        public void GetCurrent(Guid studentId)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM Enrollment WHERE StudentId = @StudentId";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@StudentId", studentId);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
                Begin = Convert.ToDateTime(dataTable.Rows[0]["Begin"]);
                End = Convert.ToDateTime(dataTable.Rows[0]["End"]);
                StudentId = Guid.Parse(dataTable.Rows[0]["StudentId"].ToString());
                Status = (EStatusEnrollment)Convert.ToInt32(dataTable.Rows[0]["Status"]);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }
        public void Get(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM Enrollment WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
                Begin = Convert.ToDateTime(dataTable.Rows[0]["Begin"]);
                End = Convert.ToDateTime(dataTable.Rows[0]["End"]);
                StudentId = Guid.Parse(dataTable.Rows[0]["StudentId"].ToString());
                Status = (EStatusEnrollment)Convert.ToInt32(dataTable.Rows[0]["Status"]);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }
    }
}