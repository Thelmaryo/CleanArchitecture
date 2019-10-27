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
        string sql;
        public StudentRepository(IDB db)
        {
            _db = db;
        }
        public void Create(Student student)
        {
            using var db = _db.GetCon();
            sql = "INSERT INTO [User] (Id, UserName, Password, Salt, Role, Active) VALUES (@Id, @UserName, @Password, @Salt, 'Student', 1)";
            db.Execute(sql, param: new
            {
                student.Id,
                student.UserName,
                student.Password,
                student.Salt
            });

            sql = "INSERT INTO Student (Id, CourseId, Birthdate, FirstName, LastName, CPF, Email, Phone, Gender, Country, City, Address) VALUES (@Id, @CourseId, @Birthdate, @FirstName, @LastName, @CPF, @Email, @Phone, @Gender, @Country, @City, @Address)";
            db.Execute(sql, param: new
            {
                student.Id,
                student.Course.CourseId,
                student.Birthdate,
                student.FirstName,
                student.LastName,
                CPF = student.CPF.Number,
                Email = student.Email.Address,
                student.Phone,
                student.Gender,
                student.Country,
                student.City,
                student.Address
            });
        }

        public void Delete(Guid id)
        {
            using var db = _db.GetCon();
            sql = "DELETE FROM Student WHERE Id = @Id";
            db.Execute(sql, param: new { Id = id });

            sql = "DELETE FROM [User] WHERE Id = @Id";
            db.Execute(sql, param: new { Id = id });
        }

        public Student Get(Guid id)
        {
            using var db = _db.GetCon();
            sql = " SELECT s.[Id]			 " +
                    " ,[UserName]			 " +
                    " ,[Password]			 " +
                    " ,[Salt]				 " +
                    " ,[Role]				 " +
                    " ,[Active]				 " +
                    " ,[FirstName]			 " +
                    " ,[LastName]			 " +
                    " ,[Phone]				 " +
                    " ,[Birthdate]			 " +
                    " ,[Gender]				 " +
                    " ,[Country]			 " +
                    " ,[City]				 " +
                    " ,[Address]			 " +
                    " ,[CourseId]			 " +
                    " ,[Email] as [Address]	 " +
                    " ,[CPF] as Number		 " +
                    " FROM [Student] as s	 " +
                    " inner Join [User] as u " +
                    " on s.Id = u.Id		 " +
                    " WHERE s.Id = @Id		 ";
            var students = db.Query<Student, Course, Email, CPF, Student>(sql,
                param: new { Id = id },
                map: (student, course, email, cpf) =>
                {
                    student = new Student(student.Id, course.CourseId, student.Birthdate, student.FirstName, student.LastName, cpf.Number, email.Address, student.Phone, student.Gender, student.Country, student.City, student.Address, student.Password, student.Salt, student.Active); ;

                    return student;
                },
            splitOn: "Id, CourseId, Address, Number");
            return students.SingleOrDefault();
        }

        public Student Get(string CPF)
        {
            using var db = _db.GetCon();
            sql = " SELECT s.[Id]			 " +
                    " ,[UserName]			 " +
                    " ,[Password]			 " +
                    " ,[Salt]				 " +
                    " ,[Role]				 " +
                    " ,[Active]				 " +
                    " ,[FirstName]			 " +
                    " ,[LastName]			 " +
                    " ,[Phone]				 " +
                    " ,[Birthdate]			 " +
                    " ,[Gender]				 " +
                    " ,[Country]			 " +
                    " ,[City]				 " +
                    " ,[Address]			 " +
                    " ,[CourseId]			 " +
                    " ,[Email] as [Address]	 " +
                    " ,[CPF] as Number		 " +
                    " FROM [Student] as s	 " +
                    " inner Join [User] as u " +
                    " on s.Id = u.Id		 " +
                    " WHERE CPF = @CPF		 ";
            var students = db.Query<Student, Course, Email, CPF, Student>(sql,
                param: new { CPF },
                map: (student, course, email, cpf) =>
                {
                    student = new Student(student.Id, course.CourseId, student.Birthdate, student.FirstName, student.LastName, cpf.Number, email.Address, student.Phone, student.Gender, student.Country, student.City, student.Address, student.Password, student.Salt, student.Active); ;

                    return student;
                },
            splitOn: "Id, CourseId, Address, Number");
            return students.SingleOrDefault();
        }

        public IEnumerable<Student> GetByDiscipline(Guid id)
        {
            using var db = _db.GetCon();
            sql = " SELECT s.[Id]	  	    " +
                    " ,[UserName]	  	    " +
                    " ,[Password]	  	    " +
                    " ,[Salt]		  	    " +
                    " ,[Role]		  	    " +
                    " ,[Active]		  	    " +
                    " ,[FirstName]	  	    " +
                    " ,[LastName]	  	    " +
                    " ,[Phone]		  	    " +
                    " ,[Birthdate]	  	    " +
                    " ,[Gender]		  	    " +
                    " ,[Country]	  	    " +
                    " ,[City]		  	    " +
                    " ,[Address]	  	    " +
                    " ,[CourseId]		    " +
                    " ,[Email] as [Address] " +
                    " ,[CPF] as Number	    " +
                    " FROM [Student] s	    " +
                    " INNER JOIN Enrollment e ON (s.Id = e.StudentId)							   " +
                    " INNER JOIN StudentDiscipline sd ON (e.Id = sd.EnrollmentId)				   " +
                    " INNER JOIN [User] u on s.Id = u.Id										   " +
                    " WHERE sd.DisciplineId = @Id												   " +
                    " AND e.Status = @EnrollmentSatus											   " +
                    " AND e.Id = (SELECT ee.Id FROM Enrollment ee WHERE ee.StudentId = e.StudentId " +
                    " AND GETDATE() BETWEEN ee.[Begin] AND ee.[End])							   ";
            var students = db.Query<Student, Course, Email, CPF, Student>(sql,
                param: new { Id = id, EnrollmentSatus = EStatusEnrollment.Confirmed },
                map: (student, course, email, cpf) =>
                {
                    student = new Student(student.Id, course.CourseId, student.Birthdate, student.FirstName, student.LastName, cpf.Number, email.Address, student.Phone, student.Gender, student.Country, student.City, student.Address, student.Password, student.Salt, student.Active); ;

                    return student;
                },
            splitOn: "Id, CourseId, Address, Number");
            return students;
        }

        public IEnumerable<Student> List()
        {
            using var db = _db.GetCon();
            sql = " SELECT s.[Id]			 " +
                    " ,[UserName]			 " +
                    " ,[Password]			 " +
                    " ,[Salt]				 " +
                    " ,[Role]				 " +
                    " ,[Active]				 " +
                    " ,[FirstName]			 " +
                    " ,[LastName]			 " +
                    " ,[Phone]				 " +
                    " ,[Birthdate]			 " +
                    " ,[Gender]				 " +
                    " ,[Country]			 " +
                    " ,[City]				 " +
                    " ,[Address]			 " +
                    " ,[CourseId]			 " +
                    " ,[Email] as [Address]	 " +
                    " ,[CPF] as Number		 " +
                    " FROM [Student] as s	 " +
                    " inner Join [User] as u " +
                    " on s.Id = u.Id		 ";
            var students = db.Query<Student, Course, Email, CPF, Student>(sql,
                map: (student, course, email, cpf) =>
                {
                    student = new Student(student.Id, course.CourseId, student.Birthdate, student.FirstName, student.LastName, cpf.Number, email.Address, student.Phone, student.Gender, student.Country, student.City, student.Address, student.Password, student.Salt, student.Active); ;

                    return student;
                },
            splitOn: "Id, CourseId, Address, Number");
            return students;
        }

        public void Update(Student student)
        {
            using var db = _db.GetCon();
            sql = "UPDATE Student SET CourseId=@CourseId, Birthdate=@Birthdate, FirstName=@FirstName, LastName=@LastName, CPF=@CPF, Email=@Email, Phone=@Phone, Gender=@Gender, Country=@Country, City=@City, Address=@Address WHERE Id = @Id";
            db.Execute(sql, param: new
            {
                student.Course.CourseId,
                student.Birthdate,
                student.FirstName,
                student.LastName,
                CPF = student.CPF.Number,
                Email = student.Email.Address,
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
