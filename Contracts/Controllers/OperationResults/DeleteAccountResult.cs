namespace Services.Controllers.OperationResults;

public abstract record DeleteAccountResult
{
    public sealed record AccountDoesntExist : DeleteAccountResult;

    public sealed record UserIsNotOwner : DeleteAccountResult;

    public sealed record BalanceGreaterThanZero : DeleteAccountResult;

    public sealed record BalanceLowerThanZero : DeleteAccountResult;

    public sealed record Success : DeleteAccountResult;
}