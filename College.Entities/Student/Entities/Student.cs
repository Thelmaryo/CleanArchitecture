using College.Entities.Account.Entities;
using College.Entities.Shared;
using System;

namespace College.Entities.Student.Entities
{
    public class Student : User
    {
        public Student(Course course, DateTime birthdate, string firstName, string lastName, string cpf, string email, string phone, string gender, string country, string city, string address)
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
            
            if (string.IsNullOrEmpty(city))
            {
                Notifications.Add("City", "A cidade é campo obrigatorio!");
            }
            if (string.IsNullOrEmpty(country))
            {
                Notifications.Add("Country", "O país é campo obrigatorio!");
            }

            Email = new Email(email);
            Notifications.Add("Email", Email.Notification);
            CPF = new CPF(cpf);
            Notifications.Add("CPF", Email.Notification);

            Course = course;
            Birthdate = birthdate;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Gender = gender;
            Country = country;
            City = city;
            Address = address;
        }

        public Course Course { get; private set; }

        public DateTime Birthdate { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public CPF CPF { get; private set; }

        public Email Email { get; private set; }

        public string Phone { get; private set; }

        public string Gender { get; private set; }

        public string Country { get; private set; }

        public string City { get; private set; }

        public string Address { get; private set; }

        // Editar Student
        public void UpdateEntity(Course course, DateTime birthdate, string firstName, string lastName, CPF cpf, Email email, string phone, string gender, string country, string city, string address)
        {
            Course = course;
            Birthdate = birthdate;
            FirstName = firstName;
            LastName = lastName;
            CPF = cpf;
            Email = email;
            Phone = phone;
            Gender = gender;
            Country = country;
            City = city;
            Address = address;
        }
    }
}
