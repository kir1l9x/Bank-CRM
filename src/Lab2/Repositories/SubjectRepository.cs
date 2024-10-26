using Itmo.ObjectOrientedProgramming.Lab2.Subjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class SubjectRepository
{
    private readonly List<ISubject> _subjects = [];

    public void Add(ISubject entity)
    {
        _subjects.Add(entity);
    }

    public SubjectResult GetById(Guid id)
    {
        foreach (ISubject subject in _subjects)
        {
            if (subject.Id == id)
            {
                return new SubjectResult.SuccessFound(subject);
            }
        }

        return new SubjectResult.FailureFound();
    }
}