using Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services;

public class LabWorkService(IUser user, ContextData contextData)
{
    public LabWork CreateLabWork(string labWorkName, string description, int points, IList<string> criteria)
    {
        LabWork.LabWorksBuilder builder = LabWork.LabWorkBuilder(user);
        builder.SetName(labWorkName).SetDescription(description).SetPoints(points);

        foreach (string criterion in criteria)
        {
            builder.AddCriteria(criterion);
        }

        LabWork labWork = builder.Build();
        contextData.LabWorksRepository.Add(labWork);

        return labWork;
    }

    public LabWork CloneCreateLabWork(LabWork labWork)
    {
        LabWork result = labWork.Clone(user);
        contextData.LabWorksRepository.Add(result);

        return result;
    }

    public LabWorkResult UpdateLabWorkName(LabWork labWork, string name)
    {
        LabWork.LabWorksBuilder builder = LabWork.LabWorkBuilder(user);
        LabWorkResult result = builder.UpdateName(labWork, name);

        return result;
    }

    public LabWorkResult UpdateLabWorkDescription(LabWork labWork, string description)
    {
        LabWork.LabWorksBuilder builder = LabWork.LabWorkBuilder(user);
        LabWorkResult result = builder.UpdateDescription(labWork, description);

        return result;
    }

    public LabWorkResult UpdateLabWorkCriteria(LabWork labWork, IReadOnlyList<string> criteria)
    {
        LabWork.LabWorksBuilder builder = LabWork.LabWorkBuilder(user);
        LabWorkResult result = builder.UpdateCriteria(labWork, criteria);

        return result;
    }

    public LabWorkResult GetLabWork(Guid labWorkId)
    {
        return contextData.LabWorksRepository.GetById(labWorkId);
    }
}