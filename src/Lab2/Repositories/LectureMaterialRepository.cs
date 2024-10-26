using Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class LectureMaterialRepository
{
    private readonly List<LectureMaterial> _lectureMaterials = [];

    public void Add(LectureMaterial entity)
    {
        _lectureMaterials.Add(entity);
    }

    public LectureResult GetById(Guid id)
    {
        foreach (LectureMaterial lectureMaterial in _lectureMaterials)
        {
            if (lectureMaterial.Id == id)
            {
                return new LectureResult.SuccessFound(lectureMaterial);
            }
        }

        return new LectureResult.FailureFound();
    }
}