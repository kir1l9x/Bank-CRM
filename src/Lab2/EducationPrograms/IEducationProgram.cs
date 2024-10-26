using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;

public interface IEducationProgram
{
    Guid Id { get; }

    string Name { get; }

    IUser ProgramManager { get; }

    IReadOnlyList<ITerm> Terms { get; }

    int TermsAmount { get; }
}