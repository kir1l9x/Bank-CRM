namespace Services.Controllers.OperationResults;

public abstract record GetAccountTransactionsResult
{
    public string TransactionsInfo { get; }

    private GetAccountTransactionsResult(string transactionsInfo)
    {
        TransactionsInfo = transactionsInfo;
    }

    private GetAccountTransactionsResult()
    {
        TransactionsInfo = string.Empty;
    }

    public sealed record Success : GetAccountTransactionsResult
    {
        public Success(string transactionsInfo) : base(transactionsInfo) { }
    }

    public sealed record AccountDoesNotExist : GetAccountTransactionsResult;

    public sealed record TransactionListIsEmpty : GetAccountTransactionsResult;
}