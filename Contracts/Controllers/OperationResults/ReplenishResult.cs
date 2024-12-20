namespace Services.Controllers.OperationResults;

public abstract record ReplenishResult
{
    public sealed record Success : ReplenishResult;

    public sealed record Failure : ReplenishResult;
}