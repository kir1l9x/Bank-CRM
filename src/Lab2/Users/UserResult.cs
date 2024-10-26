namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public abstract record UserResult
{
    public IUser? User { get; }

    private UserResult(IUser user)
    {
        User = user;
    }

    private UserResult()
    {
        User = null;
    }

    public sealed record SuccessFound : UserResult
    {
        public SuccessFound(IUser user) : base(user) { }
    }

    public sealed record FailureFound : UserResult
    {
        public FailureFound() : base() { }
    }
}