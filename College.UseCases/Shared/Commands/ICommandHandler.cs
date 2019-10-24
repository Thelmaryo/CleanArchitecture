namespace College.UseCases.Shared.Commands
{
    public interface ICommandHandler<T> where T: ICommand
    {
        public ICommandResult Handle(T command);
    }
}
