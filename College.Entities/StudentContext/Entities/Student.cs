using College.Entities.AccountContext.Entities;
using College.Entities.Shared;
using System;

namespace College.Entities.StudentContext.Entities
{
    public class Student : User
    {
        // Dapper
        public Student(){}
        // Create
        public Student(Course course, DateTime birthdate, string firstName, string lastName, string cpf, string email, string phone, string gender, string country, string city, string address, string password, string salt) : base(email, password, salt, true)
        {
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
            Validation();
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

        // Editar
        public Student(Course course, DateTime birthdate, string firstName, string lastName, string email, string phone, string gender, string country, string city, string address, Guid? id) : base(email)
        {
            if (id != null)
                Id = (Guid)id;

            Email = new Email(email);
            Notifications.Add("Email", Email.Notification);

            Course = course;
            Birthdate = birthdate;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Gender = gender;
            Country = country;
            City = city;
            Address = address;
            Validation();
        }

        private void Validation()
        {
            if (string.IsNullOrEmpty(FirstName) || FirstName.Length < 3)
                Notifications.Add("FirstName", "O Nome deve ter no minimo 3 caracteres");
            if (string.IsNullOrEmpty(LastName) || LastName.Length < 3)
                Notifications.Add("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
            if (string.IsNullOrEmpty(Phone) || Phone.Length < 8)
                Notifications.Add("Telefone", "O Telefone deve ter no minimo 8 caracteres");
            if (string.IsNullOrEmpty(City))
                Notifications.Add("City", "A cidade é campo obrigatorio!");
            if (string.IsNullOrEmpty(Country))
                Notifications.Add("Country", "O país é campo obrigatorio!");
        }

    }
}
