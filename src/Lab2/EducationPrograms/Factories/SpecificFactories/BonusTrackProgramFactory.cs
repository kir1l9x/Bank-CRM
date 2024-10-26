using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.SpecificEducationPrograms.AdditionalEducationPrograms;
using Itmo.ObjectOrientedProgramming.Lab2.Ensures;
using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Factories.SpecificFactories;

public class BonusTrackProgramFactory : IEducationProgramFactory
{
    private const int TermsAmount = 6;

    private readonly string _providingCompanyName;

    private readonly string _facultyName;

    private readonly IUser _manager;

    public BonusTrackProgramFactory(IUser manager, string providingCompanyName, string facultyName)
    {
        Ensure.NotEmpty(providingCompanyName, nameof(providingCompanyName));
        Ensure.NotEmpty(facultyName, nameof(facultyName));

        _manager = manager;
        _providingCompanyName = providingCompanyName;
        _facultyName = facultyName;
    }

    public EducationProgramResult CreateEducationProgram(string name, IReadOnlyList<ITerm> terms)
    {
        Ensure.NotEmpty(name, nameof(name));

        if (terms.Count != TermsAmount)
        {
            return new EducationProgramResult.EducationProgramMustHaveCertainTerms(
                new BonusTrackEducationProgram(name, _manager, terms, _providingCompanyName, _facultyName));
        }

        for (int i = 1; i <= TermsAmount; i++)
        {
            if (terms[i - 1].Number != i)
            {
                return new EducationProgramResult.BrokenTermsOrder(
                    new BonusTrackEducationProgram(name, _manager, terms, _providingCompanyName, _facultyName));
            }
        }

        return new EducationProgramResult.Success(new BonusTrackEducationProgram(name, _manager, terms, _providingCompanyName, _facultyName));
    }
}