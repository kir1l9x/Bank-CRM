using Entities.Users;

namespace Services.Controllers.OperationResults;

public abstract record AuthenticationResult
{
    public User? AuthenticatedUser { get; }

    private AuthenticationResult(User? user)
    {
        AuthenticatedUser = user;
    }

    private AuthenticationResult()
    {
        AuthenticatedUser = null;
    }

    public sealed record Success : AuthenticationResult
    {
        public Success(User user) : base(user) { }
    }

    public sealed record UserWithNameDoesNotExist : AuthenticationResult;

    public sealed record WrongPassword : AuthenticationResult;

    public sealed record UserWithNameExists : AuthenticationResult;
}