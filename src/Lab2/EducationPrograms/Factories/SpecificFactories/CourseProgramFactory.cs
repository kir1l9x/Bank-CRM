using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.SpecificEducationPrograms.AdditionalEducationPrograms;
using Itmo.ObjectOrientedProgramming.Lab2.Ensures;
using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Factories.SpecificFactories;

public class CourseProgramFactory : IEducationProgramFactory
{
    private const int TermsAmount = 3;

    private readonly string _providingCompanyName;

    private readonly IUser _manager;

    public CourseProgramFactory(IUser manager, string providingCompanyName)
    {
        Ensure.NotEmpty(providingCompanyName, nameof(providingCompanyName));

        _manager = manager;
        _providingCompanyName = providingCompanyName;
    }

    public EducationProgramResult CreateEducationProgram(string name, IReadOnlyList<ITerm> terms)
    {
        Ensure.NotEmpty(name, nameof(name));

        if (terms.Count != TermsAmount)
        {
            return new EducationProgramResult.EducationProgramMustHaveCertainTerms(
                new CourseEducationProgram(name, _manager, terms, _providingCompanyName));
        }

        for (int i = 1; i <= TermsAmount; i++)
        {
            if (terms[i - 1].Number != i)
            {
                return new EducationProgramResult.BrokenTermsOrder(
                    new CourseEducationProgram(name, _manager, terms, _providingCompanyName));
            }
        }

        return new EducationProgramResult.Success(new CourseEducationProgram(name, _manager, terms, _providingCompanyName));
    }
}