using Itmo.ObjectOrientedProgramming.Lab2.Ensures;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;

public class LectureMaterial
{
    private LectureMaterial(Guid id, string name, string description, IUser user, Guid? baseId, string content)
    {
        Id = id;
        Name = name;
        Description = description;
        Owner = user;
        BaseId = baseId;
        Content = content;
    }

    public Guid Id { get; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public IUser Owner { get; }

    public Guid? BaseId { get; }

    public string Content { get; private set; }

    public static LectureMaterialsBuilder LectureMaterialBuilder(IUser user)
    {
        return new LectureMaterialsBuilder(user);
    }

    public class LectureMaterialsBuilder(IUser owner)
    {
        private Guid? _id;

        private string? _name;

        private string? _description;

        private Guid? _baseId;

        private string? _content;

        public LectureMaterialsBuilder CreateId()
        {
            _id = Guid.NewGuid();
            return this;
        }

        public LectureMaterialsBuilder SetName(string name)
        {
            Ensure.NotEmpty(name, nameof(name));

            _name = name;
            return this;
        }

        public LectureMaterialsBuilder SetDescription(string description)
        {
            Ensure.NotEmpty(description, nameof(description));

            _description = description;
            return this;
        }

        public LectureMaterialsBuilder SetBaseId(Guid baseId)
        {
            _baseId = baseId;
            return this;
        }

        public LectureMaterialsBuilder SetContent(string content)
        {
            Ensure.NotEmpty(content, nameof(content));

            _content = content;
            return this;
        }

        public LectureResult UpdateName(LectureMaterial lecture, string name)
        {
            if (lecture.Owner != owner)
            {
                return new LectureResult.UserIsNotOwner(lecture);
            }

            Ensure.NotEmpty(name, nameof(name));

            lecture.Name = name;

            return new LectureResult.Success(lecture);
        }

        public LectureResult UpdateDescription(LectureMaterial lecture, string description)
        {
            if (lecture.Owner != owner)
            {
                return new LectureResult.UserIsNotOwner(lecture);
            }

            Ensure.NotEmpty(description, nameof(description));

            lecture.Description = description;

            return new LectureResult.Success(lecture);
        }

        public LectureResult UpdateContent(LectureMaterial lecture, string content)
        {
            if (lecture.Owner != owner)
            {
                return new LectureResult.UserIsNotOwner(lecture);
            }

            Ensure.NotEmpty(content, nameof(content));

            lecture.Content = content;

            return new LectureResult.Success(lecture);
        }

        public LectureMaterial Build()
        {
            return new LectureMaterial(
                _id ?? Guid.NewGuid(),
                _name ?? throw new ArgumentNullException(),
                _description ?? throw new ArgumentNullException(),
                owner,
                _baseId,
                _content ?? throw new ArgumentNullException());
        }
    }

    public LectureMaterial Clone(IUser user)
    {
        return new LectureMaterial(
            Guid.NewGuid(),
            Name,
            Description,
            user,
            Id,
            Content);
    }
}