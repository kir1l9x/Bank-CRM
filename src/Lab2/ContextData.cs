using Itmo.ObjectOrientedProgramming.Lab2.Repositories;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public class ContextData
{
    public LabWorkRepository LabWorksRepository { get; set; } = new LabWorkRepository();

    public LectureMaterialRepository LectureMaterialsRepository { get; set; } = new LectureMaterialRepository();

    public EducationProgramRepository EducationProgramsRepository { get; set; } = new EducationProgramRepository();

    public SubjectRepository SubjectsRepository { get; set; } = new SubjectRepository();

    public UserRepository UsersRepository { get; set; } = new UserRepository();
}