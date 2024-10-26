using Itmo.ObjectOrientedProgramming.Lab2.Services;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public class Controller
{
    private readonly ContextData _data;

    private IUser _currentUser;

    public Controller(IUser currentUser)
    {
        _currentUser = currentUser;
        _data = new ContextData();
        LabWorks = new LabWorkService(_currentUser, _data);
        Lectures = new LectureMaterialService(_currentUser, _data);
        Subjects = new SubjectService(_currentUser, _data);
        EducationProgram = new EducationProgramService(_currentUser, _data);
        TermService = new TermService();
        UserService = new UserService(_data);
    }

    public LabWorkService LabWorks { get; private set; }

    public LectureMaterialService Lectures { get; private set; }

    public SubjectService Subjects { get; private set; }

    public EducationProgramService EducationProgram { get; private set; }

    public TermService TermService { get; private set; }

    public UserService UserService { get; private set; }

    public void ChangeCurrentUser(IUser user)
    {
        _currentUser = user;
        LabWorks = new LabWorkService(_currentUser, _data);
        Lectures = new LectureMaterialService(_currentUser, _data);
        Subjects = new SubjectService(_currentUser, _data);
        EducationProgram = new EducationProgramService(_currentUser, _data);
        TermService = new TermService();
        UserService = new UserService(_data);
    }
}