using Itmo.ObjectOrientedProgramming.Lab2.Repositories.SpecificRepositories;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public class ContextData
{
    public LabWorkRepository LabWorksRepository { get; } = new LabWorkRepository();

    public LectureMaterialRepository LectureMaterialsRepository { get; } = new LectureMaterialRepository();

    public EducationProgramRepository EducationProgramsRepository { get;  } = new EducationProgramRepository();

    public SubjectRepository SubjectsRepository { get; } = new SubjectRepository();

    public UserRepository UsersRepository { get; set; } = new UserRepository();
}