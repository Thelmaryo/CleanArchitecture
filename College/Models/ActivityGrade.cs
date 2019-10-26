using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace College.Models
{
    public class ActivityGrade : IRepository
    {
        public Guid Id { get; set; }
        [Display(Name = "Acadêmico")]
        public Guid StudentId { get; set; }
        [Display(Name = "Atividade")]
        public Guid ActivityId { get; set; }
        [Display(Name = "Nota")]
        public decimal Grade { get; set; }

        private readonly SqlConnection _db = new SqlConnection("Server=DESKTOP-N1T1LL1; database=College; User Id=sa; Password=123");

        public void Create()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "INSERT INTO StudentActivity (Id, StudentId, ActivityId, Grade) VALUES (@Id, @StudentId, @ActivityId, @Grade)";
            SqlCommand command = new SqlCommand(sql, _db);
            Id = Guid.NewGuid();
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@StudentId", StudentId);
            command.Parameters.AddWithValue("@ActivityId", ActivityId);
            command.Parameters.AddWithValue("@Grade", Grade);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "UPDATE StudentActivity SET Grade = @Grade WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Grade", Grade);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void GetByStudent(Guid studentId, Guid activityId)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM StudentActivity WHERE StudentId = @StudentId AND ActivityId = @ActivityId";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@StudentId", studentId);
            command.Parameters.AddWithValue("@ActivityId", activityId);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
                ActivityId = Guid.Parse(dataTable.Rows[0]["ActivityId"].ToString());
                StudentId = Guid.Parse(dataTable.Rows[0]["StudentId"].ToString());
                Grade = Convert.ToDecimal(dataTable.Rows[0]["Grade"]);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }
    }
}