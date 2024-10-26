namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public abstract record SubjectResult
{
    public ISubject? Subject { get; }

    private SubjectResult(ISubject subject)
    {
        Subject = subject;
    }

    private SubjectResult()
    {
        Subject = null;
    }

    public sealed record Success : SubjectResult
    {
        public Success(ISubject subject) : base(subject) { }
    }

    public sealed record SubjectMustHaveHundredPoints : SubjectResult
    {
        public SubjectMustHaveHundredPoints() : base() { }
    }

    public sealed record UserIsNotOwner : SubjectResult
    {
        public UserIsNotOwner(ISubject subject) : base(subject) { }
    }

    public sealed record SuccessFound : SubjectResult
    {
        public SuccessFound(ISubject subject) : base(subject) { }
    }

    public sealed record FailureFound : SubjectResult
    {
        public FailureFound() : base() { }
    }
}