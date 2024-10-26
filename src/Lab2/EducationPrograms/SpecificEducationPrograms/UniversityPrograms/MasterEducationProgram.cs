using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.SpecificEducationPrograms.UniversityPrograms;

public class MasterEducationProgram : IEducationProgram
{
    public Guid Id { get; }

    public string Name { get; }

    public IUser ProgramManager { get; }

    public IReadOnlyList<ITerm> Terms { get; } = [];

    public int TermsAmount { get; } = 4;

    public MasterEducationProgram(string name, IUser user, IReadOnlyList<ITerm> terms)
    {
        Id = Guid.NewGuid();
        Name = name;
        ProgramManager = user;
        Terms = terms;
    }
}