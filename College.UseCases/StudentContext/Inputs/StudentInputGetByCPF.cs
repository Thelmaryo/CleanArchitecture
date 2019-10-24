using College.UseCases.Shared.Commands;

namespace College.UseCases.StudentContext.Inputs
{
    public class StudentInputGetByCPF : ICommand
    {
        public string StudentCPF { get; set; }
    }
}
