using Itmo.ObjectOrientedProgramming.Lab2.Ensures;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects;

public class CreditSubject : ISubject
{
    private readonly IReadOnlyList<LabWork> _labWorks = [];

    private readonly List<LectureMaterial> _lectures = [];

    private int _creditPoints;

    private CreditSubject(Guid id, string name, IUser user, int creditPoints, Guid? baseId, IReadOnlyList<LabWork> labWorks, IList<LectureMaterial> lectures)
    {
        Id = id;
        Name = name;
        Owner = user;
        _creditPoints = creditPoints;
        BaseId = baseId;
        _labWorks = labWorks;
        _lectures = lectures.ToList();
    }

    public Guid Id { get; }

    public string Name { get; private set; }

    public IUser Owner { get; }

    public Guid? BaseId { get; }

    public IReadOnlyList<LectureMaterial> Lectures => _lectures;

    public static CreditSubjectsBuilder CreditSubjectBuilder(IUser owner)
    {
        return new CreditSubjectsBuilder(owner);
    }

    public class CreditSubjectsBuilder(IUser owner) : BaseSubjectBuilder(owner)
    {
        private readonly IUser _owner = owner;

        public override ISubjectBuilder SetPoints(int points)
        {
            Ensure.Positive(points, nameof(points));

            if (points > 100)
            {
                throw new ArgumentException("The points must be lower or equal to 100.");
            }

            Points = points;

            return this;
        }

        public SubjectResult UpdateCreditSubjectName(CreditSubject subject, string name)
        {
            if (subject.Owner != _owner)
            {
                return new SubjectResult.UserIsNotOwner(subject);
            }

            Ensure.NotEmpty(name, nameof(name));

            subject.Name = name;

            return new SubjectResult.Success(subject);
        }

        public SubjectResult UpdateCreditPoints(CreditSubject subject, int points)
        {
            if (subject.Owner != _owner)
            {
                return new SubjectResult.UserIsNotOwner(subject);
            }

            Ensure.Positive(points, nameof(points));

            subject._creditPoints = points;

            return new SubjectResult.Success(subject);
        }

        public SubjectResult AddLecture(CreditSubject subject, LectureMaterial lecture)
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
            int sumPoints = 0;

            foreach (LabWork labWork in labWorks)
            {
                sumPoints += labWork.Points;
            }

            if (sumPoints != 100)
            {
                return new SubjectResult.SubjectMustHaveHundredPoints();
            }

            return new SubjectResult.Success(new CreditSubject(id, name, owner, points, baseId, labWorks.AsReadOnly(), lectures));
        }
    }

    public ISubject Clone(IUser user)
    {
        return new CreditSubject(
            Guid.NewGuid(),
            Name,
            user,
            _creditPoints,
            Id,
            new List<LabWork>(_labWorks).AsReadOnly(),
            new List<LectureMaterial>(_lectures).AsReadOnly());
    }
}