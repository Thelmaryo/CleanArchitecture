﻿using System;
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
        string sql;
        public ProfessorRepository(IDB db)
        {
            _db = db;
        }
        public void Create(Professor professor)
        {
            using var db = _db.GetCon();
            sql = "INSERT INTO [User] (Id, UserName, Password, Active, Salt, Role) VALUES (@Id, @UserName, @Password, @Salt, 1, 'Professor')";
            db.Execute(sql, param: new
            {
                professor.Id,
                professor.UserName,
                professor.Password,
                professor.Salt
            });

            sql = "INSERT INTO Professor (Id, FirstName, LastName, Degree, CPF, Email, Phone) VALUES (@Id, @FirstName, @LastName, @Degree, @CPF, @Email, @Phone)";
            db.Execute(sql, param: new
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

        public void Disable(Guid id)
        {
            using var db = _db.GetCon();
            sql = "UPDATE [User] Set Active = 0 WHERE Id = @Id";
            db.Execute(sql, param: new
            {
                Id = id
            });
        }

        public Professor Get(Guid id)
        {
            using var db = _db.GetCon();
            sql = " SELECT [Id]		 " +
                "       ,[FirstName] " +
                "       ,[LastName]	 " +
                "       ,[Phone]	 " +
                "       ,[CPF]		 " +
                "       ,[Email]	 " +
                "       ,[Degree]	 " +
                "   FROM [Professor] " +
                "   WHERE Id = @Id	 ";
            var professors = db.Query<Professor, CPF, Email, EDegree, Professor>(sql,
                param: new { Id = id },
                map: (professor, cpf, email, eDegree) =>
                {
                    cpf = new CPF(cpf.Number);
                    email = new Email(email.Address);
                    eDegree = (EDegree)professor.Degree;
                    professor.UpdateEntity(professor.FirstName, professor.LastName, cpf.Number, professor.Email.Address, professor.Phone, eDegree);

                    return professor;
                },
            splitOn: "Id, CPF, Email, Degree");
            return professors.SingleOrDefault();
        }

        public int GetWorkload(Guid professorId)
        {
            using var db = _db.GetCon();
            var sql = "SELECT SUM(WeeklyWorkload) AS Workload FROM Discipline WHERE ProfessorId = @Id";
            var workload = db.QuerySingleOrDefault<int>(sql, param: new { Id = professorId });
            return workload;
        }

        public IEnumerable<Professor> List()
        {
            using var db = _db.GetCon();
            sql = " SELECT [Id]		 " +
                "       ,[FirstName] " +
                "       ,[LastName]	 " +
                "       ,[Phone]	 " +
                "       ,[CPF]		 " +
                "       ,[Email]	 " +
                "       ,[Degree]	 " +
                "   FROM [Professor] ";
            var professors = db.Query<Professor, CPF, Email, EDegree, Professor>(sql,
                map: (professor, cpf, email, eDegree) =>
                {
                    cpf = new CPF(cpf.Number);
                    email = new Email(email.Address);
                    eDegree = (EDegree)professor.Degree;
                    professor.UpdateEntity(professor.FirstName, professor.LastName, cpf.Number, professor.Email.Address, professor.Phone, eDegree);

                    return professor;
                }, splitOn: "Id, CPF, Email, Degree");
            return professors;
        }

        public void Update(Professor professor)
        {
            using var db = _db.GetCon();
            sql = "UPDATE Professor SET FirstName = @FirstName, LastName = @LastName, CPF = @CPF, Email = @Email, Phone = @Phone, Degree = @Degree WHERE Id = @Id";
            db.Execute(sql, param: new
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