using College.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace College.Models
{
    public class Student : User, IRepository
    {
        [Display(Name = "Course")]
        public Guid CourseId { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Nome")]
        public string FirstName { get; set; }
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        [Display(Name = "Telefone")]
        public string Phone { get; set; }
        [Display(Name = "Gênero")]
        public string Gender { get; set; }
        [Display(Name = "País")]
        public string Country { get; set; }
        [Display(Name = "Cidade")]
        public string City { get; set; }
        [Display(Name = "Endereço")]
        public string Address { get; set; }

        private readonly SqlConnection _db = new SqlConnection("Server=DESKTOP-N1T1LL1; database=College; User Id=sa; Password=123");

        public void Create()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "INSERT INTO [User] (Id, UserName, Password, Salt, Role, Active) VALUES (@Id, @UserName, @Password, @Salt, 'Student', 1)";
            SqlCommand command = new SqlCommand(sql, _db);
            Id = Guid.NewGuid();
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@UserName", Email);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Salt", Salt);
            command.ExecuteNonQuery();
            sql = "INSERT INTO Student (Id, CourseId, Birthdate, FirstName, LastName, CPF, Email, Phone, Gender, Country, City, Address) VALUES (@Id, @CourseId, @Birthdate, @FirstName, @LastName, @CPF, @Email, @Phone, @Gender, @Country, @City, @Address)";
            command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@CourseId", CourseId);
            command.Parameters.AddWithValue("@Birthdate", Birthdate);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@CPF", CPF);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Country", Country);
            command.Parameters.AddWithValue("@City", City);
            command.Parameters.AddWithValue("@Address", Address);

            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Edit()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "UPDATE Student SET CourseId=@CourseId, Birthdate=@Birthdate, FirstName=@FirstName, LastName=@LastName, CPF=@CPF, Email=@Email, Phone=@Phone, Gender=@Gender, Country=@Country, City=@City, Address=@Address WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@CourseId", CourseId);
            command.Parameters.AddWithValue("@Birthdate", Birthdate);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@CPF", CPF);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Country", Country);
            command.Parameters.AddWithValue("@City", City);
            command.Parameters.AddWithValue("@Address", Address);

            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Delete()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "DELETE FROM Student WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
            sql = "DELETE FROM [User] WHERE Id = @Id";
            command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public IEnumerable<Student> List()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Students = new List<Student>();
            var sql = "SELECT * FROM Student";
            SqlCommand command = new SqlCommand(sql, _db);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var student = new Student
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    CourseId = Guid.Parse(dataRow["CourseId"].ToString()),
                    Birthdate = Convert.ToDateTime(dataRow["Birthdate"]),
                    FirstName = dataRow["FirstName"].ToString(),
                    LastName = dataRow["LastName"].ToString(),
                    CPF = dataRow["CPF"].ToString(),
                    Email = dataRow["Email"].ToString(),
                    Phone = dataRow["Phone"].ToString(),
                    Gender = dataRow["Gender"].ToString(),
                    Country = dataRow["Country"].ToString(),
                    City = dataRow["City"].ToString(),
                    Address = dataRow["Address"].ToString()
                };
                Students.Add(student);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Students;
        }

        public void Get(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM Student WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
                CourseId = Guid.Parse(dataTable.Rows[0]["CourseId"].ToString());
                Birthdate = Convert.ToDateTime(dataTable.Rows[0]["Birthdate"]);
                FirstName = dataTable.Rows[0]["FirstName"].ToString();
                LastName = dataTable.Rows[0]["LastName"].ToString();
                CPF = dataTable.Rows[0]["CPF"].ToString();
                Email = dataTable.Rows[0]["Email"].ToString();
                Phone = dataTable.Rows[0]["Phone"].ToString();
                Gender = dataTable.Rows[0]["Gender"].ToString();
                Country = dataTable.Rows[0]["Country"].ToString();
                City = dataTable.Rows[0]["City"].ToString();
                Address = dataTable.Rows[0]["Address"].ToString();
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Get(string CPF)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM Student WHERE CPF = @CPF";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@CPF", CPF);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
                CourseId = Guid.Parse(dataTable.Rows[0]["CourseId"].ToString());
                Birthdate = Convert.ToDateTime(dataTable.Rows[0]["Birthdate"]);
                FirstName = dataTable.Rows[0]["FirstName"].ToString();
                LastName = dataTable.Rows[0]["LastName"].ToString();
                CPF = dataTable.Rows[0]["CPF"].ToString();
                Email = dataTable.Rows[0]["Email"].ToString();
                Phone = dataTable.Rows[0]["Phone"].ToString();
                Gender = dataTable.Rows[0]["Gender"].ToString();
                Country = dataTable.Rows[0]["Country"].ToString();
                City = dataTable.Rows[0]["City"].ToString();
                Address = dataTable.Rows[0]["Address"].ToString();
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public IEnumerable<Student> GetByDiscipline(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Students = new List<Student>();
            var sql = "SELECT s.* FROM Student s INNER JOIN Enrollment e ON (s.Id = e.StudentId) INNER JOIN StudentDiscipline sd ON (e.Id = sd.EnrollmentId) WHERE sd.DisciplineId = @Id AND e.Status = @EnrollmentSatus AND e.Id = (SELECT ee.Id FROM Enrollment ee WHERE ee.Status = 1 AND ee.StudentId = e.StudentId AND GETDATE() BETWEEN ee.[Begin] AND ee.[End])";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@EnrollmentSatus", EStatusEnrollment.Confirmed);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var student = new Student
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    Birthdate = Convert.ToDateTime(dataRow["Birthdate"]),
                    FirstName = dataRow["FirstName"].ToString(),
                    LastName = dataRow["LastName"].ToString(),
                    CPF = dataRow["CPF"].ToString(),
                    Email = dataRow["Email"].ToString(),
                    Phone = dataRow["Phone"].ToString(),
                    Gender = dataRow["Gender"].ToString(),
                    Country = dataRow["Country"].ToString(),
                    City = dataRow["City"].ToString(),
                    Address = dataRow["Address"].ToString()
                };
                Students.Add(student);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Students;
        }
    }
}
