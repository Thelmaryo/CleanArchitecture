using College.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace College.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Display(Name = "Usuário")]
        public string UserName { get; set; }
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public bool Active { get; set; }

        private readonly SqlConnection _db = new SqlConnection("Server=DESKTOP-23IN36H; database=College; User Id=sa; Password=123");

        public void Login()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM [User] WHERE UserName = @UserName AND Password = @Password AND Active = 1";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (_db.State == ConnectionState.Open)
                _db.Close();
            Authentication.UserAuthenticated = (dataTable.Rows.Count > 0 && dataTable.Rows[0]["Id"] != DBNull.Value);
            if (Authentication.UserAuthenticated)
                Authentication.UserId = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
        }

        public void Logout()
        {
            Authentication.UserAuthenticated = false;
        }

        public bool IsInRole(string role)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT COUNT(*) AS Result FROM [User] WHERE Id = @Id AND Role = @Role";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Role", role);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Convert.ToInt32(dataTable.Rows[0]["Result"]) == 1;
        }
    }
}