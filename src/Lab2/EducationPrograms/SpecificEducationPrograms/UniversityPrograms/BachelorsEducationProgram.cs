using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.SpecificEducationPrograms.UniversityPrograms;

public class BachelorsEducationProgram : IEducationProgram
{
    public Guid Id { get; }

    public string Name { get; }

    public IUser ProgramManager { get; }

    public IReadOnlyList<ITerm> Terms { get; }

    public int TermsAmount { get; } = 8;

    public BachelorsEducationProgram(string name, IUser programManager, IReadOnlyList<ITerm> terms)
    {
        Id = Guid.NewGuid();
        Name = name;
        ProgramManager = programManager;
        Terms = terms;
    }
}