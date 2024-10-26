using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.SpecificEducationPrograms.AdditionalEducationPrograms;

public class CourseEducationProgram : IAdditionalEducationProgram
{
    public Guid Id { get; }

    public string Name { get; }

    public IUser ProgramManager { get; }

    public IReadOnlyList<ITerm> Terms { get; }

    public string ProvidingCompanyName { get; }

    public int TermsAmount { get; } = 3;

    public CourseEducationProgram(
        string name,
        IUser programManager,
        IReadOnlyList<ITerm> terms,
        string providingCompanyName)
    {
        Id = Guid.NewGuid();
        Name = name;
        ProgramManager = programManager;
        Terms = terms;
        ProvidingCompanyName = providingCompanyName;
    }
}