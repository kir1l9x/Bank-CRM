using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class EducationProgramRepository
{
    private readonly List<IEducationProgram> _educationPrograms = [];

    public void Add(IEducationProgram entity)
    {
        _educationPrograms.Add(entity);
    }

    public EducationProgramResult GetById(Guid id)
    {
        foreach (IEducationProgram educationProgram in _educationPrograms)
        {
            if (educationProgram.Id == id)
            {
                return new EducationProgramResult.SuccessFound(educationProgram);
            }
        }

        return new EducationProgramResult.FailureFound();
    }
}