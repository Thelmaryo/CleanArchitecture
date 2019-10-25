using System;
using System.Collections.Generic;
using System.Linq;
using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.Entities.Shared;
using College.Infra.DataSource;
using College.UseCases.ProfessorContext.Repositories;
using Dapper;

namespace College.Infra.ProfessorContext
{
    public class ProfessorRepository : IProfessorRepository
    {
        IDB _db;
        public ProfessorRepository(IDB db)
        {
            _db = db;
        }
        public void Create(Professor professor)
        {
            using (var db = _db.GetCon())
            {
                var sql = "INSERT INTO [User] (Id, UserName, Password, Active, Salt, Role) VALUES (@Id, @UserName, @Password, @Salt, 1, 'Professor')";
                db.Execute(sql, new
                {
                    professor.Id,
                    professor.UserName,
                    professor.Password,
                    professor.Salt
                });

                sql = "INSERT INTO Professor (Id, FirstName, LastName, Degree, CPF, Email, Phone) VALUES (@Id, @FirstName, @LastName, @Degree, @CPF, @Email, @Phone)";
                db.Execute(sql, new
                {
                    professor.Id,
                    professor.FirstName,
                    professor.LastName,
                    professor.Degree,
                    professor.CPF,
                    professor.Email,
                    professor.Phone
                });
            }
        }

        public void Disable(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = "UPDATE [User] Set Active = 0 WHERE Id = @Id";
                db.Execute(sql, new
                {
                    Id = id
                });
            }
        }

        public Professor Get(Guid id)
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		 " +
                        "       ,[FirstName] " +
                        "       ,[LastName]	 " +
                        "       ,[Phone]	 " +
                        "       ,[CPF]		 " +
                        "       ,[Email]	 " +
                        "       ,[Degree]	 " +
                        "   FROM [Professor] " +
                        "   WHERE Id = @Id	 ";
                var professor = db.Query<Professor, CPF, Email, EDegree, Professor>(sql,
                    map: (professor, cpf, email, eDegree) =>
                    {
                        cpf = new CPF(cpf.Number);
                        email = new Email(email.Address);
                        eDegree = (EDegree)professor.Degree;
                        professor = new Professor(professor.FirstName, professor.LastName, professor.Email.Address, professor.Phone, eDegree);

                        return professor;
                    }, new { Id = id },
                splitOn: "Id, CPF, Email, Degree");
                return professor.FirstOrDefault();
            }
        }

        public int GetWorkload(Guid professorId)
        {
            using (var db = _db.GetCon())
            {
                var sql = "SELECT SUM(WeeklyWorkload) AS Workload FROM Discipline WHERE ProfessorId = @Id";
                return db.QueryFirstOrDefault<int>(sql, new { Id = professorId });
            }
        }

        public IEnumerable<Professor> List()
        {
            using (var db = _db.GetCon())
            {
                var sql = " SELECT [Id]		 " +
                        "       ,[FirstName] " +
                        "       ,[LastName]	 " +
                        "       ,[Phone]	 " +
                        "       ,[CPF]		 " +
                        "       ,[Email]	 " +
                        "       ,[Degree]	 " +
                        "   FROM [Professor] ";
                var professor = db.Query<Professor, CPF, Email, EDegree, Professor>(sql,
                    map: (professor, cpf, email, eDegree) =>
                    {
                        cpf = new CPF(cpf.Number);
                        email = new Email(email.Address);
                        eDegree = (EDegree)professor.Degree;
                        professor = new Professor(professor.FirstName, professor.LastName, professor.Email.Address, professor.Phone, eDegree);

                        return professor;
                    }, splitOn: "Id, CPF, Email, Degree");
                return professor;
            }
        }

        public void Update(Professor professor)
        {
            using (var db = _db.GetCon())
            {
                var sql = "UPDATE Professor SET FirstName = @FirstName, LastName = @LastName, CPF = @CPF, Email = @Email, Phone = @Phone, Degree = @Degree WHERE Id = @Id";
                db.Execute(sql, new
                {
                    professor.FirstName,
                    professor.LastName,
                    professor.CPF,
                    professor.Email,
                    professor.Phone,
                    professor.Degree,
                    professor.Id
                });
            }
        }
    }
}
