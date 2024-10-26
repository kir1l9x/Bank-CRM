namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;

public abstract record EducationProgramResult
{
    public IEducationProgram? Program { get; }

    private EducationProgramResult(IEducationProgram program)
    {
        Program = program;
    }

    private EducationProgramResult()
    {
        Program = null;
    }

    public sealed record Success : EducationProgramResult
    {
        public Success(IEducationProgram program) : base(program) { }
    }

    public sealed record EducationProgramMustHaveCertainTerms : EducationProgramResult
    {
        public EducationProgramMustHaveCertainTerms(IEducationProgram program) : base(program) { }
    }

    public sealed record BrokenTermsOrder : EducationProgramResult
    {
        public BrokenTermsOrder(IEducationProgram program) : base(program) { }
    }

    public sealed record SuccessFound : EducationProgramResult
    {
        public SuccessFound(IEducationProgram program) : base(program) { }
    }

    public sealed record FailureFound : EducationProgramResult
    {
        public FailureFound() : base() { }
    }
}