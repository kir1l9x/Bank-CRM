using Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services;

public class LectureMaterialService(IUser user, ContextData contextData)
{
    public LectureMaterial CreateLectureMaterial(string lectureMaterialName, string description, string content)
    {
        LectureMaterial.LectureMaterialsBuilder builder = LectureMaterial.LectureMaterialBuilder(user);
        LectureMaterial lectureMaterial = builder.SetName(lectureMaterialName).SetDescription(description).SetContent(content).Build();
        contextData.LectureMaterialsRepository.Add(lectureMaterial);

        return lectureMaterial;
    }

    public LectureMaterial CloneCreateLectureMaterial(LectureMaterial lectureMaterial)
    {
        LectureMaterial result = lectureMaterial.Clone(user);
        contextData.LectureMaterialsRepository.Add(result);

        return result;
    }

    public LectureResult UpdateLectureMaterialName(LectureMaterial lecture, string name)
    {
        LectureMaterial.LectureMaterialsBuilder builder = LectureMaterial.LectureMaterialBuilder(user);
        LectureResult result = builder.UpdateName(lecture, name);

        return result;
    }

    public LectureResult UpdateLectureMaterialDescription(LectureMaterial lecture, string description)
    {
        LectureMaterial.LectureMaterialsBuilder builder = LectureMaterial.LectureMaterialBuilder(user);
        LectureResult result = builder.UpdateDescription(lecture, description);

        return result;
    }

    public LectureResult UpdateLectureMaterialContent(LectureMaterial lecture, string content)
    {
        LectureMaterial.LectureMaterialsBuilder builder = LectureMaterial.LectureMaterialBuilder(user);
        LectureResult result = builder.UpdateContent(lecture, content);

        return result;
    }

    public LectureResult GetLectureMaterial(Guid lectureId)
    {
        return contextData.LectureMaterialsRepository.GetById(lectureId);
    }
}