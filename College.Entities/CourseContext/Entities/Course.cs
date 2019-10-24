﻿using College.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace College.Entities.CourseContext.Entities
{
    public class Course : Entity
    {
        public Course(string name)
        {
            Name = name;
        }

        [Display(Name = "Nome")]
        public string Name { get; private set; }

        // Editar Course
        public void UpdateEntity(string name)
        {
            Name = name;
        }
    }
}