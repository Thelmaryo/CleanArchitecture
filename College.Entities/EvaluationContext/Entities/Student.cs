﻿using College.Entities.Shared;
using System;

namespace College.Entities.EvaluationContext.Entities
{
    public class Student : Entity
    {
        public Student(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Student(Guid id)
        {
            Id = id;
        }

        public string Name { get; private set; }
    }
}
