﻿using College.Presenters.Shared;
using System;
using System.Collections.Generic;

namespace College.Presenters.EnrollmentContext
{
    public class CreateEnrollmentViewModel
    {
        public Guid StudentId { get; set; }
        public IEnumerable<Checkbox> Disciplines { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
