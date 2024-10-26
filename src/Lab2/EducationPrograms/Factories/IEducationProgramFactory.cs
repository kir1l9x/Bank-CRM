using Itmo.ObjectOrientedProgramming.Lab2.Terms;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Factories;

public interface IEducationProgramFactory
{
    EducationProgramResult CreateEducationProgram(string name, IReadOnlyList<ITerm> terms);
}
