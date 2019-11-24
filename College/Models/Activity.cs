using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace College.Models
{
    public class Activity : IRepository
    {
        public Guid Id { get; set; }
        [Display(Name = "Disciplina")]
        public Guid DisciplineId { get; set; }
        [Display(Name = "Atividade")]
        public string Description { get; set; }
        [Display(Name = "Valor")]
        public decimal Value { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }


        private readonly SqlConnection _db = new SqlConnection("Server=DESKTOP-23IN36H; database=College; User Id=sa; Password=123");

        public void Create()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "INSERT INTO Activity (Id, DisciplineId, Description, Value, [Date]) VALUES (@Id, @DisciplineId, @Description, @Value, @Date)";
            SqlCommand command = new SqlCommand(sql, _db);
            Id = Guid.NewGuid();
            command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@DisciplineId", DisciplineId);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Value", Value);
            command.Parameters.AddWithValue("@Date", Date);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Edit()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "UPDATE Activity SET Description = @Description, Value = @Value, [Date] = @Date WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Value", Value);
            command.Parameters.AddWithValue("@Date", Date);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Delete()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "DELETE FROM Activity WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Get(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM Activity WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
                DisciplineId = Guid.Parse(dataTable.Rows[0]["DisciplineId"].ToString());
                Description = dataTable.Rows[0]["Description"].ToString();
                Value = Convert.ToDecimal(dataTable.Rows[0]["Value"]);
                Date = Convert.ToDateTime(dataTable.Rows[0]["Date"]);
            }

            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public IEnumerable<Activity> GetByDiscipline(Guid id, Semester semester)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Activities = new List<Activity>();
            var sql = "SELECT * FROM Activity WHERE DisciplineId = @Id AND [Date] BETWEEN @Begin AND @End";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Begin", semester.Begin);
            command.Parameters.AddWithValue("@End", semester.End);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var exam = new Activity
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    DisciplineId = Guid.Parse(dataRow["DisciplineId"].ToString()),
                    Description = dataRow["Description"].ToString(),
                    Value = Convert.ToDecimal(dataRow["Value"]),
                    Date = Convert.ToDateTime(dataRow["Date"])
                };
                Activities.Add(exam);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Activities;
        }
    }
}

