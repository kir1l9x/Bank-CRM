using Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class LabWorkRepository : BaseRepository<LabWork, LabWorkResult>
{
    public override LabWorkResult GetById(Guid id)
    {
        foreach (LabWork labWork in Items)
        {
            if (labWork.Id == id)
            {
                return new LabWorkResult.SuccessFound(labWork);
            }
        }

        return new LabWorkResult.FailureFound();
    }
}