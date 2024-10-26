using Itmo.ObjectOrientedProgramming.Lab2.Ensures;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;

public abstract class BaseSubjectBuilder(IUser owner) : ISubjectBuilder
{
    private readonly List<LabWork> _labWorks = [];

    private readonly List<LectureMaterial> _lectures = [];

    private Guid? _id;

    private string? _name;

    protected int? Points { get; set; }

    public ISubjectBuilder CreateId()
    {
        _id = Guid.NewGuid();
        return this;
    }

    public ISubjectBuilder SetName(string name)
    {
        Ensure.NotEmpty(name, nameof(name));

        _name = name;
        return this;
    }

    public ISubjectBuilder AddLecture(LectureMaterial lecture)
    {
        _lectures.Add(lecture);
        return this;
    }

    public ISubjectBuilder AddLabWork(LabWork labWork)
    {
        _labWorks.Add(labWork);
        return this;
    }

    public SubjectResult Build()
    {
        return Build(
            _id ?? Guid.NewGuid(),
            _name ?? throw new ArgumentNullException(),
            owner,
            Points ?? throw new ArgumentNullException(),
            null,
            _labWorks,
            _lectures);
    }

    public abstract ISubjectBuilder SetPoints(int points);

    protected abstract SubjectResult Build(
        Guid id,
        string name,
        IUser owner,
        int points,
        Guid? baseId,
        IList<LabWork> labWorks,
        IList<LectureMaterial> lectures);
}