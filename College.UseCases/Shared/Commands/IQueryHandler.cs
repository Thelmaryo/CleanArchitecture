namespace College.UseCases.Shared.Commands
{
    public interface IQueryHandler<T, R> where T: ICommand where R : IQueryResult
    {
        R Handle(T command);
    }
}
