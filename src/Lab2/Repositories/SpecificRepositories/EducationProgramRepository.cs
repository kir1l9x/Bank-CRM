using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class EducationProgramRepository : BaseRepository<IEducationProgram, EducationProgramResult>
{
    public override EducationProgramResult GetById(Guid id)
    {
        foreach (IEducationProgram educationProgram in Items)
        {
            if (educationProgram.Id == id)
            {
                return new EducationProgramResult.SuccessFound(educationProgram);
            }
        }

        return new EducationProgramResult.FailureFound();
    }
}