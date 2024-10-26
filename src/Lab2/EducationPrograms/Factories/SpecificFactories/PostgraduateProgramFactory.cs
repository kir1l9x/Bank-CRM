using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.SpecificEducationPrograms.UniversityPrograms;
using Itmo.ObjectOrientedProgramming.Lab2.Ensures;
using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Factories.SpecificFactories;

public class PostgraduateProgramFactory(IUser manager) : IEducationProgramFactory
{
    private const int TermsAmount = 2;

    public EducationProgramResult CreateEducationProgram(string name, IReadOnlyList<ITerm> terms)
    {
        Ensure.NotEmpty(name, nameof(name));

        if (terms.Count != TermsAmount)
        {
            return new EducationProgramResult.EducationProgramMustHaveCertainTerms(
                new PostgraduateEducationProgram(name, manager, terms));
        }

        for (int i = 1; i <= TermsAmount; i++)
        {
            if (terms[i - 1].Number != i)
            {
                return new EducationProgramResult.BrokenTermsOrder(
                    new PostgraduateEducationProgram(name, manager, terms));
            }
        }

        return new EducationProgramResult.Success(new PostgraduateEducationProgram(name, manager, terms));
    }
}