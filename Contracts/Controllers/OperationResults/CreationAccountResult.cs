using Entities.Accounts;

namespace Services.Controllers.OperationResults;

public abstract record CreationAccountResult
{
    public sealed record Success(Account CreatedAccount) : CreationAccountResult;

    public sealed record UserDoesNotExist : CreationAccountResult;
}