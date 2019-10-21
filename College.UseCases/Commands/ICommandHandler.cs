namespace College.UseCases.Commands
{
    public interface ICommandHandler<T> where T: ICommand
    {
        public ICommandResult Handle(T command);
    }
}
