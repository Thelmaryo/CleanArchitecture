using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.AccountContext.Results
{
    public class UserResultQueryLogin : IQueryResult
    {
        public Guid UserId { get; set; }
    }
}
