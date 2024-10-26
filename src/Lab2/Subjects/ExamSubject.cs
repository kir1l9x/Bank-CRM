using Itmo.ObjectOrientedProgramming.Lab2.Ensures;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class ExamSubject : ISubject
{
    private readonly IReadOnlyList<LabWork> _labWorks = [];

    private readonly List<LectureMaterial> _lectures = [];

    private readonly int _examPoints;

    private ExamSubject(Guid id, string name, IUser user, int examPoints, Guid? baseId, IReadOnlyList<LabWork> labWorks, IList<LectureMaterial> lectures)
    {
        Id = id;
        Name = name;
        Owner = user;
        _examPoints = examPoints;
        BaseId = baseId;
        _labWorks = labWorks;
        _lectures = lectures.ToList();
    }

    public Guid Id { get; }

    public string Name { get; private set; }

    public IUser Owner { get; }

    public Guid? BaseId { get; }

    public IReadOnlyList<LectureMaterial> Lectures => _lectures;

    public static ExamSubjectsBuilder ExamSubjectBuilder(IUser owner)
    {
        return new ExamSubjectsBuilder(owner);
    }

    public class ExamSubjectsBuilder(IUser owner) : BaseSubjectBuilder(owner)
    {
        private readonly IUser _owner = owner;

        public override ISubjectBuilder SetPoints(int points)
        {
            Ensure.Positive(points, nameof(points));

            if (points > 100)
            {
                throw new ArgumentException("Exam points must be greater than 0 and less than 100."); // ResType
            }

            Points = points;

            return this;
        }

        public SubjectResult UpdateExamSubjectName(ExamSubject subject, string name)
        {
            if (subject.Owner != _owner)
            {
                return new SubjectResult.UserIsNotOwner(subject);
            }

            Ensure.NotEmpty(subject.Name, nameof(name));

            subject.Name = name;

            return new SubjectResult.Success(subject);
        }

        public SubjectResult AddLecture(ExamSubject subject, LectureMaterial lecture)
        {
            if (subject.Owner != _owner)
            {
                return new SubjectResult.UserIsNotOwner(subject);
            }

            subject._lectures.Add(lecture);

            return new SubjectResult.Success(subject);
        }

        protected override SubjectResult Build(Guid id, string name, IUser owner, int points, Guid? baseId, IList<LabWork> labWorks, IList<LectureMaterial> lectures)
        {
            int sumPoints = points;

            foreach (LabWork labWork in labWorks)
            {
                sumPoints += labWork.Points;
            }

            if (sumPoints != 100)
            {
                return new SubjectResult.SubjectMustHaveHundredPoints();
            }

            return new SubjectResult.Success(new ExamSubject(id, name, owner, points, baseId, labWorks.AsReadOnly(), lectures));
        }
    }

    public ISubject Clone(IUser user)
    {
        return new ExamSubject(
            Guid.NewGuid(),
            Name,
            user,
            _examPoints,
            Id,
            new List<LabWork>(_labWorks).AsReadOnly(),
            new List<LectureMaterial>(_lectures).AsReadOnly());
    }
}