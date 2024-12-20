namespace Services.Controllers.OperationResults;

public abstract record GetAccountsResult
{
    public string AccountsInfo { get; }

    private GetAccountsResult(string accountsInfo)
    {
        AccountsInfo = accountsInfo;
    }

    private GetAccountsResult()
    {
        AccountsInfo = string.Empty;
    }

    public sealed record Success : GetAccountsResult
    {
        public Success(string accountsInfo) : base(accountsInfo) { }
    }

    public sealed record UserDoesNotExist : GetAccountsResult;

    public sealed record UserDontHaveAccounts : GetAccountsResult;
}