using Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories.SpecificRepositories;

public class LectureMaterialRepository : BaseRepository<LectureMaterial, LectureResult>
{
    public override LectureResult GetById(Guid id)
    {
        foreach (LectureMaterial lectureMaterial in Items)
        {
            if (lectureMaterial.Id == id)
            {
                return new LectureResult.SuccessFound(lectureMaterial);
            }
        }

        return new LectureResult.FailureFound();
    }
}