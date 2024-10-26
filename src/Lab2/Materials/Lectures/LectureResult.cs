namespace Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;

public abstract record LectureResult
{
    public LectureMaterial? Material { get; }

    private LectureResult(LectureMaterial material)
    {
        Material = material;
    }

    private LectureResult()
    {
        Material = null;
    }

    public sealed record Success : LectureResult
    {
        public Success(LectureMaterial material) : base(material) { }
    }

    public sealed record UserIsNotOwner : LectureResult
    {
        public UserIsNotOwner(LectureMaterial material) : base(material) { }
    }

    public sealed record SuccessFound : LectureResult
    {
        public SuccessFound(LectureMaterial material) : base(material) { }
    }

    public sealed record FailureFound : LectureResult
    {
        public FailureFound() : base() { }
    }
}