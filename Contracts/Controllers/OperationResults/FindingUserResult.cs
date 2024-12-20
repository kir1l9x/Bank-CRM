using Entities.Users;

namespace Services.Controllers.OperationResults;

public abstract record FindingUserResult
{
    public User? FoundUser { get; }

    private FindingUserResult(User foundUser)
    {
        FoundUser = foundUser;
    }

    private FindingUserResult()
    {
        FoundUser = null;
    }

    public sealed record Success : FindingUserResult
    {
        public Success(User foundUser) : base(foundUser) { }
    }

    public sealed record Failed : FindingUserResult;
}