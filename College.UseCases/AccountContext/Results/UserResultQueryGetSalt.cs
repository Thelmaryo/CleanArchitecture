using College.UseCases.Shared.Commands;

namespace College.UseCases.AccountContext.Results
{
    public class UserResultQueryGetSalt : IQueryResult
    {
        public string Salt { get; set; }
    }
}
