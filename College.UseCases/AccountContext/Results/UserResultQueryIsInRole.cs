using College.UseCases.Shared.Commands;

namespace College.UseCases.AccountContext.Results
{
    public class UserResultQueryIsInRole : IQueryResult
    {
        public bool IsInRole { get; set; }
    }
}
