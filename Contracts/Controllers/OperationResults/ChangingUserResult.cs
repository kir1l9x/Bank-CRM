namespace Services.Controllers.OperationResults;

public abstract record ChangingUserResult
{
    public sealed record Success : ChangingUserResult;

    public sealed record Failure : ChangingUserResult;
}