namespace Services.Controllers.OperationResults;

public abstract record TransferResult
{
    public sealed record IncorrectFromNumber : TransferResult;

    public sealed record IncorrectToNumber : TransferResult;

    public sealed record UserIsNotOwner : TransferResult;

    public sealed record BalanceIsLowerThanTransferAmount : TransferResult;

    public sealed record Success : TransferResult;
}