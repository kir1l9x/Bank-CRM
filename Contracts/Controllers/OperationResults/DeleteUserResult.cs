namespace Services.Controllers.OperationResults;

public abstract record DeleteUserResult
{
    public sealed record Success : DeleteUserResult;

    public sealed record Failure : DeleteUserResult;
}