using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.SpecificEducationPrograms.AdditionalEducationPrograms;

public class BonusTrackEducationProgram : IAdditionalEducationProgram
{
    public Guid Id { get; }

    public string Name { get; }

    public IUser ProgramManager { get; }

    public IReadOnlyList<ITerm> Terms { get; }

    public string ProvidingCompanyName { get; }

    public int TermsAmount { get; } = 6;

    public string FacultyName { get; }

    public BonusTrackEducationProgram(
        string name,
        IUser programManager,
        IReadOnlyList<ITerm> terms,
        string providingCompanyName,
        string facultyName)
    {
        Id = Guid.NewGuid();
        Name = name;
        ProgramManager = programManager;
        Terms = terms;
        ProvidingCompanyName = providingCompanyName;
        FacultyName = facultyName;
    }
}