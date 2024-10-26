using Itmo.ObjectOrientedProgramming.Lab2.Ensures;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;

public class LabWork
{
    private string _description;

    private IReadOnlyList<string> _criterias;

    private LabWork(Guid id, string name, string description, int points, IUser user, Guid? baseId, IReadOnlyList<string> criterias)
    {
        Id = id;
        Name = name;
        _description = description;
        Points = points;
        Owner = user;
        BaseId = baseId;
        _criterias = criterias;
    }

    public Guid Id { get; }

    public string Name { get; private set; }

    public int Points { get; }

    public IUser Owner { get; }

    public Guid? BaseId { get; }

    public static LabWorksBuilder LabWorkBuilder(IUser user)
    {
        return new LabWorksBuilder(user);
    }

    public class LabWorksBuilder(IUser owner)
    {
        private readonly List<string> _criterias = [];

        private string? _name;

        private string? _description;

        private int? _points;

        private Guid? _baseId;

        public LabWorksBuilder SetName(string name)
        {
            Ensure.NotEmpty(name, nameof(name));

            _name = name;
            return this;
        }

        public LabWorksBuilder SetDescription(string description)
        {
            Ensure.NotEmpty(description, nameof(description));

            _description = description;
            return this;
        }

        public LabWorksBuilder SetPoints(int points)
        {
            Ensure.Positive(points, nameof(points));

            _points = points;
            return this;
        }

        public LabWorksBuilder SetBaseId(Guid baseId)
        {
            _baseId = baseId;
            return this;
        }

        public LabWorksBuilder AddCriteria(string criteria)
        {
            Ensure.NotEmpty(criteria, nameof(criteria));

            _criterias.Add(criteria);
            return this;
        }

        public LabWorkResult UpdateCriteria(LabWork labWork, IReadOnlyList<string> criteria)
        {
            if (labWork.Owner.Id != owner.Id)
            {
                return new LabWorkResult.UserIsNotOwner(labWork);
            }

            labWork._criterias = criteria;

            return new LabWorkResult.Success(labWork);
        }

        public LabWorkResult UpdateDescription(LabWork labWork, string description)
        {
            if (labWork.Owner.Id != owner.Id)
            {
                return new LabWorkResult.UserIsNotOwner(labWork);
            }

            Ensure.NotEmpty(description, nameof(description));

            labWork._description = description;

            return new LabWorkResult.Success(labWork);
        }

        public LabWorkResult UpdateName(LabWork labWork, string name)
        {
            if (labWork.Owner.Id != owner.Id)
            {
                return new LabWorkResult.UserIsNotOwner(labWork);
            }

            Ensure.NotEmpty(name, nameof(name));

            labWork.Name = name;

            return new LabWorkResult.Success(labWork);
        }

        public LabWork Build()
        {
            return new LabWork(
                Guid.NewGuid(),
                _name ?? throw new ArgumentNullException(),
                _description ?? throw new ArgumentNullException(),
                _points ?? throw new ArgumentNullException(),
                owner ?? throw new ArgumentNullException(),
                _baseId,
                _criterias);
        }
    }

    public LabWork Clone(IUser user)
    {
        return new LabWork(
            Guid.NewGuid(),
            Name,
            _description,
            Points,
            user,
            Id,
            new List<string>(_criterias).AsReadOnly());
    }
}