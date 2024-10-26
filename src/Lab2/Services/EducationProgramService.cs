using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;
using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Factories.SpecificFactories;
using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services;

public class EducationProgramService(IUser user, ContextData contextData)
{
    public EducationProgramResult CreateBachelorsEducationProgram(string name, IReadOnlyList<ITerm> terms)
    {
        var factory = new BachelorsProgramFactory(user);

        EducationProgramResult bachelorsProgram = factory.CreateEducationProgram(name, terms);

        if (bachelorsProgram is EducationProgramResult.Success)
        {
            if (bachelorsProgram.Program != null)
            {
                contextData.EducationProgramsRepository.Add(bachelorsProgram.Program);
            }
        }

        return bachelorsProgram;
    }

    public EducationProgramResult CreateMasterEducationProgram(string name, IReadOnlyList<ITerm> terms)
    {
        var factory = new MasterProgramFactory(user);

        EducationProgramResult masterProgram = factory.CreateEducationProgram(name, terms);

        if (masterProgram is EducationProgramResult.Success)
        {
            if (masterProgram.Program != null)
            {
                contextData.EducationProgramsRepository.Add(masterProgram.Program);
            }
        }

        return masterProgram;
    }

    public EducationProgramResult CreatePostgraduateEducationProgram(string name, IReadOnlyList<ITerm> terms)
    {
        var factory = new PostgraduateProgramFactory(user);

        EducationProgramResult postgraduateProgram = factory.CreateEducationProgram(name, terms);

        if (postgraduateProgram is EducationProgramResult.Success)
        {
            if (postgraduateProgram.Program != null)
            {
                contextData.EducationProgramsRepository.Add(postgraduateProgram.Program);
            }
        }

        return postgraduateProgram;
    }

    public EducationProgramResult CreateCourseEducationProgram(
        string name,
        IReadOnlyList<ITerm> terms,
        string providingCompanyName)
    {
        var factory = new CourseProgramFactory(user, providingCompanyName);

        EducationProgramResult courseProgram = factory.CreateEducationProgram(name, terms);

        if (courseProgram is EducationProgramResult.Success)
        {
            if (courseProgram.Program != null)
            {
                contextData.EducationProgramsRepository.Add(courseProgram.Program);
            }
        }

        return courseProgram;
    }

    public EducationProgramResult CreateBonusTrackEducationProgram(
        string name,
        IReadOnlyList<ITerm> terms,
        string providingCompanyName,
        string facultyName)
    {
        var factory = new BonusTrackProgramFactory(user, providingCompanyName, facultyName);

        EducationProgramResult bonusProgram = factory.CreateEducationProgram(name, terms);

        if (bonusProgram is EducationProgramResult.Success)
        {
            if (bonusProgram.Program != null)
            {
                contextData.EducationProgramsRepository.Add(bonusProgram.Program);
            }
        }

        return bonusProgram;
    }

    public EducationProgramResult GetEducationProgram(Guid educationProgramId)
    {
        return contextData.EducationProgramsRepository.GetById(educationProgramId);
    }
}