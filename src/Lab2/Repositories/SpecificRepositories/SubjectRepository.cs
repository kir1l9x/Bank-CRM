using Itmo.ObjectOrientedProgramming.Lab2.Subjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories.SpecificRepositories;

public class SubjectRepository : BaseRepository<ISubject, SubjectResult>
{
    public override SubjectResult GetById(Guid id)
    {
        foreach (ISubject subject in Items)
        {
            if (subject.Id == id)
            {
                return new SubjectResult.SuccessFound(subject);
            }
        }

        return new SubjectResult.FailureFound();
    }
}