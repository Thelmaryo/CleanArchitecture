using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.StudentContext.Inputs
{
    public class StudentInputGetById : ICommand
    {
        public Guid StudentId { get; set; }
    }
}
