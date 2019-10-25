using System;
using System.Collections.Generic;
using System.Linq;
using College.Entities.EnrollmentContext.Enumerators;
using College.Entities.Shared;
using College.Entities.StudentContext.Entities;
using College.Infra.DataSource;
using College.UseCases.StudentContext.Repositories;
using Dapper;

namespace College.Infra.StudentContext
{
    public class StudentRepository : IStudentRepository
    {
        IDB _db;
        public StudentRepository(IDB db)
        {
            _db = db;
        }
        public void Create(Student student)
        {
            using (var db = _db.GetCon())
            {
                var sql = "INSERT INTO [User] (Id, UserName, Password, Salt, Role, Active) VALUES (@Id, @UserName, @Password, @Salt, 'Student', 1)";
                db.Execute(sql, new
                {
                    student.Id,
                    student.UserName,
                    student.Password,
                    student.Salt
                });

                sql = "INSERT INTO Student (Id, CourseId, Birthdate, FirstName, LastName, CPF, Email, Phone, Gender, Country, City, Address) VALUES (@Id, @CourseId, @Birthdate, @FirstName, @LastName, @CPF, @Email, @Phone, @Gender, @Country, @City, @Address)";
                db.Execute(sql, new
                {
                    student.Id,
                    student.Course.CourseId,
                    student.Birthdate,
                    student.FirstName,
                    student.LastName,
                    student.CPF,
                    student.Email,
                    student.Phone,
                    student.Gender,
                    student.Country,
                    student.City,
                    student.Address
                });
            }
        }

        public void Delete(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = "DELETE FROM Student WHERE Id = @Id";
                db.Execute(sql, new { Id = id });

                sql = "DELETE FROM [User] WHERE Id = @Id";
                db.Execute(sql, new { Id = id });
            }
        }

        public Student Get(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		 " +
                        "       ,[FirstName] " +
                        "       ,[LastName]	 " +
                        "       ,[Phone]	 " +
                        "       ,[Birthdate] " +
                        "       ,[Gender]	 " +
                        "       ,[Country]	 " +
                        "       ,[City]		 " +
                        "       ,[Address]	 " +
                        "       ,[CourseId]	 " +
                        "       ,[CPF]		 " +
                        "       ,[Email]	 " +
                        "   FROM [Student]	 " +
                        "	WHERE Id = @Id	 ";
                var student = db.Query<Student, Course, Email, CPF, Student>(sql,
                    map: (student, course, email, cpf) =>
                    {
                        course = new Course(course.CourseId);
                        cpf = new CPF(cpf.Number);
                        email = new Email(email.Address);
                        student = new Student(course, student.Birthdate, student.FirstName, student.LastName, email.Address, student.Phone, student.Gender, student.Country, student.City, student.Address, student.Id);

                        return student;
                    }, new { Id = id },
                splitOn: "Id, CourseId, CPF, Email");
                return student.FirstOrDefault();
            }
        }

        public Student Get(string CPF)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		 " +
                        "       ,[FirstName] " +
                        "       ,[LastName]	 " +
                        "       ,[Phone]	 " +
                        "       ,[Birthdate] " +
                        "       ,[Gender]	 " +
                        "       ,[Country]	 " +
                        "       ,[City]		 " +
                        "       ,[Address]	 " +
                        "       ,[CourseId]	 " +
                        "       ,[CPF]		 " +
                        "       ,[Email]	 " +
                        "   FROM [Student]	 " +
                        "	WHERE CPF = @CPF	 ";
                var student = db.Query<Student, Course, Email, CPF, Student>(sql,
                    map: (student, course, email, cpf) =>
                    {
                        course = new Course(course.CourseId);
                        cpf = new CPF(cpf.Number);
                        email = new Email(email.Address);
                        student = new Student(course, student.Birthdate, student.FirstName, student.LastName, email.Address, student.Phone, student.Gender, student.Country, student.City, student.Address, student.Id);

                        return student;
                    }, new { CPF = CPF },
                splitOn: "Id, CourseId, CPF, Email");
                return student.FirstOrDefault();
            }
        }

        public IEnumerable<Student> GetByDiscipline(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		 " +
                        "       ,[FirstName] " +
                        "       ,[LastName]	 " +
                        "       ,[Phone]	 " +
                        "       ,[Birthdate] " +
                        "       ,[Gender]	 " +
                        "       ,[Country]	 " +
                        "       ,[City]		 " +
                        "       ,[Address]	 " +
                        "       ,[CourseId]	 " +
                        "       ,[CPF]		 " +
                        "       ,[Email]	 " +
                        "   FROM [Student] s " +
                        " INNER JOIN Enrollment e ON (s.Id = e.StudentId) 							   " +
                        " INNER JOIN StudentDiscipline sd ON (e.Id = sd.EnrollmentId) 				   " +
                        " WHERE sd.DisciplineId = @Id 												   " +
                        " AND e.Status = @EnrollmentSatus 											   " +
                        " AND e.Id = (SELECT ee.Id FROM Enrollment ee WHERE ee.StudentId = e.StudentId " +
                        " AND GETDATE() BETWEEN ee.[Begin] AND ee.[End])							   ";
                var student = db.Query<Student, Course, Email, CPF, Student>(sql,
                    map: (student, course, email, cpf) =>
                    {
                        course = new Course(course.CourseId);
                        cpf = new CPF(cpf.Number);
                        email = new Email(email.Address);
                        student = new Student(course, student.Birthdate, student.FirstName, student.LastName, email.Address, student.Phone, student.Gender, student.Country, student.City, student.Address, student.Id);

                        return student;
                    }, new { Id = id, EnrollmentSatus = EStatusEnrollment.Confirmed },
                splitOn: "Id, CourseId, CPF, Email");
                return student;
            }
        }

        public IEnumerable<Student> List()
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		 " +
                        "       ,[FirstName] " +
                        "       ,[LastName]	 " +
                        "       ,[Phone]	 " +
                        "       ,[Birthdate] " +
                        "       ,[Gender]	 " +
                        "       ,[Country]	 " +
                        "       ,[City]		 " +
                        "       ,[Address]	 " +
                        "       ,[CourseId]	 " +
                        "       ,[CPF]		 " +
                        "       ,[Email]	 " +
                        "   FROM [Student]	 ";
                var student = db.Query<Student, Course, Email, CPF, Student>(sql,
                    map: (student, course, email, cpf) =>
                     {
                         course = new Course(course.CourseId);
                         cpf = new CPF(cpf.Number);
                         email = new Email(email.Address);
                         student = new Student(course, student.Birthdate, student.FirstName, student.LastName, email.Address, student.Phone, student.Gender, student.Country, student.City, student.Address, student.Id);

                         return student;
                     });
                return student;
            }
        }

        public void Update(Student student)
        {
            using (var db = _db.GetCon())
            {
                var sql = "UPDATE Student SET CourseId=@CourseId, Birthdate=@Birthdate, FirstName=@FirstName, LastName=@LastName, CPF=@CPF, Email=@Email, Phone=@Phone, Gender=@Gender, Country=@Country, City=@City, Address=@Address WHERE Id = @Id";
                db.Execute(sql, new
                {
                    student.Course.CourseId,
                    student.Birthdate,
                    student.FirstName,
                    student.LastName,
                    student.CPF,
                    student.Email,
                    student.Phone,
                    student.Gender,
                    student.Country,
                    student.City,
                    student.Address,
                    student.Id,
                });
            }
        }
    }
}
