namespace Services.Controllers.OperationResults;

public abstract record CheckBalanceResult
{
    public string Balance { get; }

    private CheckBalanceResult(string balance)
    {
        Balance = balance;
    }

    private CheckBalanceResult()
    {
        Balance = string.Empty;
    }

    public sealed record Success : CheckBalanceResult
    {
        public Success(string balance) : base(balance) { }
    }

    public sealed record AccountDoesNotExist : CheckBalanceResult;

    public sealed record UserNotOwner : CheckBalanceResult;
}