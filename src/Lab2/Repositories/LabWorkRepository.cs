using Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class LabWorkRepository
{
    private readonly List<LabWork> _labWorks = [];

    public void Add(LabWork entity)
    {
        _labWorks.Add(entity);
    }

    public LabWorkResult GetById(Guid id)
    {
        foreach (LabWork labWork in _labWorks)
        {
            if (labWork.Id == id)
            {
                return new LabWorkResult.SuccessFound(labWork);
            }
        }

        return new LabWorkResult.FailureFound();
    }
}