namespace Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;

public abstract record LabWorkResult
{
    public LabWork? LabWork { get; }

    private LabWorkResult(LabWork labWork)
    {
        LabWork = labWork;
    }

    private LabWorkResult()
    {
        LabWork = null;
    }

    public sealed record Success : LabWorkResult
    {
        public Success(LabWork labWork) : base(labWork) { }
    }

    public sealed record UserIsNotOwner : LabWorkResult
    {
        public UserIsNotOwner(LabWork labWork) : base(labWork) { }
    }

    public sealed record SuccessFound : LabWorkResult
    {
        public SuccessFound(LabWork labWork) : base(labWork) { }
    }

    public sealed record FailureFound : LabWorkResult
    {
        public FailureFound() : base() { }
    }
}