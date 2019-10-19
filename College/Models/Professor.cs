using College.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace College.Models
{
    public class Professor : User, IRepository
    {
        public string Name { get => $"{FirstName} {LastName}"; }
        [Required]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Titulação")]
        public EDegree Degree { get; set; }

        private readonly SqlConnection _db = new SqlConnection("Server=DESKTOP-23IN36H; database=College; User Id=sa; Password=123");

        public void Create()
        {
            if(_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "INSERT INTO [User] (Id, UserName, Password, Active, Role) VALUES (@Id, @UserName, @Password, 1, 'Professor')";
            SqlCommand command = new SqlCommand(sql, _db);
            Id = Guid.NewGuid();
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@UserName", Email);
            command.Parameters.AddWithValue("@Password", Password);
            command.ExecuteNonQuery();
            sql = "INSERT INTO Professor (Id, FirstName, LastName, Degree, CPF, Email, Phone) VALUES (@Id, @FirstName, @LastName, @Degree, @CPF, @Email, @Phone)";
            command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CPF", CPF);
            command.Parameters.AddWithValue("@Degree", Degree);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Edit()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "UPDATE Professor SET FirstName = @FirstName, LastName = @LastName, CPF = @CPF, Email = @Email, Phone = @Phone, Degree = @Degree WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CPF", CPF);
            command.Parameters.AddWithValue("@Degree", Degree);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }
        public void Delete()
        {
            throw new NotImplementedException();
        }
        public void Disable()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "UPDATE [User] Set Active = 0 WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public IEnumerable<Professor> List()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Professors = new List<Professor>();
            var sql = "SELECT * FROM Professor";
            SqlCommand command = new SqlCommand(sql, _db);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach(DataRow dataRow in dataTable.Rows)
            {
                var professor = new Professor {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    FirstName = dataRow["FirstName"].ToString(),
                    LastName = dataRow["LastName"].ToString(),
                    CPF = dataRow["CPF"].ToString(),
                    Phone = dataRow["Phone"].ToString(),
                    Email = dataRow["Email"].ToString(),
                    Degree = (EDegree)Convert.ToInt32(dataRow["Degree"])
                };
                Professors.Add(professor);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Professors;
        }

        public void Get(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM Professor WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
            FirstName = dataTable.Rows[0]["FirstName"].ToString();
            LastName = dataTable.Rows[0]["LastName"].ToString();
            CPF = dataTable.Rows[0]["CPF"].ToString();
            Phone = dataTable.Rows[0]["Phone"].ToString();
            Email = dataTable.Rows[0]["Email"].ToString();
            Degree = (EDegree)Convert.ToInt32(dataTable.Rows[0]["Degree"]);
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public int GetWorkload()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT SUM(WeeklyWorkload) AS Workload FROM Discipline WHERE ProfessorId = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (_db.State == ConnectionState.Open)
                _db.Close();
            if (dataTable.Rows[0]["Workload"] == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(dataTable.Rows[0]["Workload"]);
        }
    }
}