﻿using College.Entities.AccountContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.Entities.Shared;

namespace College.Entities.ProfessorContext.Entities
{
    public class Professor : User
    {
        public Professor(){}
        public Professor(string firstName, string lastName, string cpf, string email, string phone, EDegree degree, string password) : base(email, password)
        {

            if (string.IsNullOrEmpty(firstName) || firstName.Length < 3)
            {
                Notifications.Add("FirstName", "O Nome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(lastName) || lastName.Length < 3)
            {
                Notifications.Add("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(phone) || phone.Length < 8)
            {
                Notifications.Add("Telefone", "O Telefone deve ter no minimo 8 caracteres");
            }

            Email = new Email(email);
            Notifications.Add("Email", Email.Notification);
            CPF = new CPF(cpf);
            Notifications.Add("CPF", Email.Notification);

            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Degree = degree;
        }
        
        public Professor(string firstName, string lastName, string email, string phone, EDegree degree) : base(email)
        {

            if (string.IsNullOrEmpty(firstName) || firstName.Length < 3)
            {
                Notifications.Add("FirstName", "O Nome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(lastName) || lastName.Length < 3)
            {
                Notifications.Add("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(phone) || phone.Length < 8)
            {
                Notifications.Add("Telefone", "O Telefone deve ter no minimo 8 caracteres");
            }

            Email = new Email(email);
            Notifications.Add("Email", Email.Notification);

            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Degree = degree;
        }

        public string Name { get => $"{FirstName} {LastName}"; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public CPF CPF { get; private set; }

        public Email Email { get; private set; }

        public string Phone { get; private set; }

        public EDegree Degree { get; private set; }

        // Editar Professor
        public void UpdateEntity(string firstName, string lastName, string cpf, string email, string phone, EDegree degree)
        {

            if (string.IsNullOrEmpty(firstName) || firstName.Length < 3)
            {
                Notifications.Add("FirstName", "O Nome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(lastName) || lastName.Length < 3)
            {
                Notifications.Add("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(phone) || phone.Length < 8)
            {
                Notifications.Add("Telefone", "O Telefone deve ter no minimo 8 caracteres");
            }

            Email = new Email(email);
            Notifications.Add("Email", Email.Notification);
            CPF = new CPF(cpf);
            Notifications.Add("CPF", Email.Notification);

            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Degree = degree;
        }
    }
}
