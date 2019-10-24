
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace College.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        private readonly SqlConnection _db = new SqlConnection("Server=DESKTOP-23IN36H; database=College; User Id=sa; Password=123");
        public IEnumerable<Course> List()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Courses = new List<Course>();
            var sql = "SELECT * FROM Course";
            SqlCommand command = new SqlCommand(sql, _db);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var course = new Course
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    Name = dataRow["Name"].ToString()
                };
                Courses.Add(course);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Courses;
        }
    }
}