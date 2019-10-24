namespace College.UseCases.Shared.Commands
{
    public interface IQueryHandler<T, R> where T: ICommand where R : ICommandResult
    {
        public R Handle(T command);
    }
}
