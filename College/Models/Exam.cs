using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace College.Models
{
    public class Exam : IRepository
    {
        public Guid Id { get; set; }
        [Display(Name = "Matrícula")]
        public Guid EnrollmentId { get; set; }
        [Display(Name = "Disciplina")]
        public Guid DisciplineId { get; set; }
        [Display(Name = "Prova 1")]
        public decimal Exam1 { get; set; }
        [Display(Name = "Prova 2")]
        public decimal Exam2 { get; set; }
        [Display(Name = "Prova 3")]
        public decimal Exam3 { get; set; }
        [Display(Name = "Exame Final")]
        public decimal FinalExam { get; set; }

        private readonly SqlConnection _db = new SqlConnection("Server=DESKTOP-23IN36H; database=College; User Id=sa; Password=123");

        public void Create()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "INSERT INTO Exam (Id, EnrollmentId, DisciplineId, Exam1, Exam2, Exam3, FinalExam) VALUES (@Id, @EnrollmentId, @DisciplineId, @Exam1, @Exam2, @Exam3, @FinalExam)";
            Id = Guid.NewGuid();
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@EnrollmentId", EnrollmentId);
            command.Parameters.AddWithValue("@DisciplineId", DisciplineId);
            command.Parameters.AddWithValue("@Exam1", Exam1);
            command.Parameters.AddWithValue("@Exam2", Exam2);
            command.Parameters.AddWithValue("@Exam3", Exam3);
            command.Parameters.AddWithValue("@FinalExam", FinalExam);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Edit()
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "UPDATE Exam SET Exam1 = @Exam1, Exam2 = @Exam2, Exam3 = @Exam3, FinalExam = @FinalExam WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@Exam1", Exam1);
            command.Parameters.AddWithValue("@Exam2", Exam2);
            command.Parameters.AddWithValue("@Exam3", Exam3);
            command.Parameters.AddWithValue("@FinalExam", FinalExam);
            command.ExecuteNonQuery();
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public void Delete()
        {
            throw new NotImplementedException(); 
        }
        public void Get(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM Exam WHERE Id = @Id";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
                EnrollmentId = Guid.Parse(dataTable.Rows[0]["EnrollmentId"].ToString());
                DisciplineId = Guid.Parse(dataTable.Rows[0]["DisciplineId"].ToString());
                Exam1 = Convert.ToDecimal(dataTable.Rows[0]["Exam1"]);
                Exam2 = Convert.ToDecimal(dataTable.Rows[0]["Exam2"]);
                Exam3 = Convert.ToDecimal(dataTable.Rows[0]["Exam3"]);
                FinalExam = Convert.ToDecimal(dataTable.Rows[0]["FinalExam"]);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }

        public IEnumerable<Exam> GetByDiscipline(Guid id)
        {
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var Exams = new List<Exam>();
            var sql = "SELECT * FROM Exam INNER JOIN Enrollment e ON (Exam.EnrollmentId = e.Id) WHERE Exam.DisciplineId = @Id AND GetDate() BETWEEN [Begin] AND [End]";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var exam = new Exam
                {
                    Id = Guid.Parse(dataRow["Id"].ToString()),
                    EnrollmentId = Guid.Parse(dataRow["EnrollmentId"].ToString()),
                    DisciplineId = Guid.Parse(dataRow["DisciplineId"].ToString()),
                    Exam1 = Convert.ToDecimal(dataRow["Exam1"]),
                    Exam2 = Convert.ToDecimal(dataRow["Exam2"]),
                    Exam3 = Convert.ToDecimal(dataRow["Exam3"]),
                    FinalExam = Convert.ToDecimal(dataRow["FinalExam"])
                };
                Exams.Add(exam);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
            return Exams;
        }

        public void GetByStudent(Guid enrollmentId, Guid disciplineId)
        { 
            if (_db.State == ConnectionState.Closed)
                _db.Open();
            var sql = "SELECT * FROM Exam WHERE DisciplineId = @DisciplineId AND EnrollmentId = @EnrollmentId";
            SqlCommand command = new SqlCommand(sql, _db);
            command.Parameters.AddWithValue("@EnrollmentId", enrollmentId);
            command.Parameters.AddWithValue("@DisciplineId", disciplineId);
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                Id = Guid.Parse(dataTable.Rows[0]["Id"].ToString());
                EnrollmentId = Guid.Parse(dataTable.Rows[0]["EnrollmentId"].ToString());
                DisciplineId = Guid.Parse(dataTable.Rows[0]["DisciplineId"].ToString());
                Exam1 = Convert.ToDecimal(dataTable.Rows[0]["Exam1"]);
                Exam2 = Convert.ToDecimal(dataTable.Rows[0]["Exam2"]);
                Exam3 = Convert.ToDecimal(dataTable.Rows[0]["Exam3"]);
                FinalExam = Convert.ToDecimal(dataTable.Rows[0]["FinalExam"]);
            }
            if (_db.State == ConnectionState.Open)
                _db.Close();
        }
    }
}

