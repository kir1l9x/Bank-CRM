using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;
using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Factories.SpecificFactories;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Terms;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public class UserService
{
    public ContextUser CurrentUser { get; set; }

    private readonly ContextData _data;

    public UserService(ContextUser currentUser)
    {
        CurrentUser = currentUser;
        _data = new ContextData();
        _data.UsersRepository.Add(CurrentUser.User);
    }

    public IUser Login(string name)
    {
        IUser user = new UniversityUser(name);
        CurrentUser = new ContextUser(user);
        _data.UsersRepository.Add(user);

        return user;
    }

    public LabWork CreateLabWork(string labWorkName, string description, int points, IList<string> criteria)
    {
        LabWork.LabWorksBuilder builder = LabWork.LabWorkBuilder(CurrentUser.User);

        builder.CreateId().SetName(labWorkName).SetDescription(description).SetPoints(points);

        foreach (string criterion in criteria)
        {
            builder.AddCriteria(criterion);
        }

        LabWork labWork = builder.Build();

        _data.LabWorksRepository.Add(labWork);

        return labWork;
    }

    public LectureMaterial CreateLectureMaterial(string lectureMaterialName, string description, string content)
    {
        LectureMaterial.LectureMaterialsBuilder builder = LectureMaterial.LectureMaterialBuilder(CurrentUser.User);

        LectureMaterial lectureMaterial = builder.CreateId().SetName(lectureMaterialName).SetDescription(description).SetContent(content).Build();

        _data.LectureMaterialsRepository.Add(lectureMaterial);

        return lectureMaterial;
    }

    public SubjectResult CreateCreditSubject(string subjectName, IList<LabWork> labWorks, IList<LectureMaterial> lectures, int points)
    {
        CreditSubject.CreditSubjectsBuilder builder = CreditSubject.CreditSubjectBuilder(CurrentUser.User);

        builder.CreateId().SetName(subjectName).SetPoints(points);

        foreach (LabWork labWork in labWorks)
        {
            builder.AddLabWork(labWork);
        }

        foreach (LectureMaterial lecture in lectures)
        {
            builder.AddLecture(lecture);
        }

        SubjectResult creditSubject = builder.Build();

        if (creditSubject is SubjectResult.Success)
        {
            if (creditSubject.Subject != null)
            {
                _data.SubjectsRepository.Add(creditSubject.Subject);
            }
        }

        return creditSubject;
    }

    public SubjectResult CreateExamSubject(string subjectName, IList<LabWork> labWorks, IList<LectureMaterial> lectures, int points)
    {
        ExamSubject.ExamSubjectsBuilder builder = ExamSubject.ExamSubjectBuilder(CurrentUser.User);

        builder.CreateId().SetName(subjectName).SetPoints(points);

        foreach (LabWork labWork in labWorks)
        {
            builder.AddLabWork(labWork);
        }

        foreach (LectureMaterial lecture in lectures)
        {
            builder.AddLecture(lecture);
        }

        SubjectResult examSubject = builder.Build();

        if (examSubject is SubjectResult.Success)
        {
            if (examSubject.Subject != null)
            {
                _data.SubjectsRepository.Add(examSubject.Subject);
            }
        }

        return examSubject;
    }

    public ITerm CreateTerm(int termNumber, IList<ISubject> subjects)
    {
        Term.TermsBuilder builder = Term.TermBuilder;

        builder.SetTermNumber(termNumber);

        foreach (ISubject subject in subjects)
        {
            builder.AddSubject(subject);
        }

        return builder.Build();
    }

    public EducationProgramResult CreateBachelorsEducationProgram(string name, IReadOnlyList<ITerm> terms)
    {
        var factory = new BachelorsProgramFactory(CurrentUser.User);

        EducationProgramResult bachelorsProgram = factory.CreateEducationProgram(name, terms);

        if (bachelorsProgram is EducationProgramResult.Success)
        {
            if (bachelorsProgram.Program != null)
            {
                _data.EducationProgramsRepository.Add(bachelorsProgram.Program);
            }
        }

        return bachelorsProgram;
    }

    public EducationProgramResult CreateMasterEducationProgram(string name, IReadOnlyList<ITerm> terms)
    {
        var factory = new MasterProgramFactory(CurrentUser.User);

        EducationProgramResult masterProgram = factory.CreateEducationProgram(name, terms);

        if (masterProgram is EducationProgramResult.Success)
        {
            if (masterProgram.Program != null)
            {
                _data.EducationProgramsRepository.Add(masterProgram.Program);
            }
        }

        return masterProgram;
    }

    public EducationProgramResult CreatePostgraduateEducationProgram(string name, IReadOnlyList<ITerm> terms)
    {
        var factory = new PostgraduateProgramFactory(CurrentUser.User);

        EducationProgramResult postgraduateProgram = factory.CreateEducationProgram(name, terms);

        if (postgraduateProgram is EducationProgramResult.Success)
        {
            if (postgraduateProgram.Program != null)
            {
                _data.EducationProgramsRepository.Add(postgraduateProgram.Program);
            }
        }

        return postgraduateProgram;
    }

    public EducationProgramResult CreateCourseEducationProgram(
        string name,
        IReadOnlyList<ITerm> terms,
        string providingCompanyName)
    {
        var factory = new CourseProgramFactory(CurrentUser.User, providingCompanyName);

        EducationProgramResult courseProgram = factory.CreateEducationProgram(name, terms);

        if (courseProgram is EducationProgramResult.Success)
        {
            if (courseProgram.Program != null)
            {
                _data.EducationProgramsRepository.Add(courseProgram.Program);
            }
        }

        return courseProgram;
    }

    public EducationProgramResult CreateBonusTrackEducationProgram(
        string name,
        IReadOnlyList<ITerm> terms,
        string providingCompanyName,
        string facultyName)
    {
        var factory = new BonusTrackProgramFactory(CurrentUser.User, providingCompanyName, facultyName);

        EducationProgramResult bonusProgram = factory.CreateEducationProgram(name, terms);

        if (bonusProgram is EducationProgramResult.Success)
        {
            if (bonusProgram.Program != null)
            {
                _data.EducationProgramsRepository.Add(bonusProgram.Program);
            }
        }

        return bonusProgram;
    }

    public LabWork CloneCreateLabWork(LabWork labWork)
    {
        LabWork result = labWork.Clone(CurrentUser.User);

        _data.LabWorksRepository.Add(result);

        return result;
    }

    public LectureMaterial CloneCreateLectureMaterial(LectureMaterial lectureMaterial)
    {
        LectureMaterial result = lectureMaterial.Clone(CurrentUser.User);

        _data.LectureMaterialsRepository.Add(result);

        return result;
    }

    public ISubject CloneCreateCreditSubject(CreditSubject creditSubject)
    {
        ISubject result = creditSubject.Clone(CurrentUser.User);

        _data.SubjectsRepository.Add(result);

        return result;
    }

    public ISubject CloneCreateExamSubject(ExamSubject examSubject)
    {
        ISubject result = examSubject.Clone(CurrentUser.User);

        _data.SubjectsRepository.Add(result);

        return result;
    }

    public LabWorkResult UpdateLabWorkName(LabWork labWork, string name)
    {
        LabWork.LabWorksBuilder builder = LabWork.LabWorkBuilder(CurrentUser.User);

        LabWorkResult result = builder.UpdateName(labWork, name);

        return result;
    }

    public LabWorkResult UpdateLabWorkDescription(LabWork labWork, string description)
    {
        LabWork.LabWorksBuilder builder = LabWork.LabWorkBuilder(CurrentUser.User);

        LabWorkResult result = builder.UpdateDescription(labWork, description);

        return result;
    }

    public LabWorkResult UpdateLabWorkCriteria(LabWork labWork, IReadOnlyList<string> criteria)
    {
        LabWork.LabWorksBuilder builder = LabWork.LabWorkBuilder(CurrentUser.User);

        LabWorkResult result = builder.UpdateCriteria(labWork, criteria);

        return result;
    }

    public LectureResult UpdateLectureMaterialName(LectureMaterial lecture, string name)
    {
        LectureMaterial.LectureMaterialsBuilder builder = LectureMaterial.LectureMaterialBuilder(CurrentUser.User);

        LectureResult result = builder.UpdateName(lecture, name);

        return result;
    }

    public LectureResult UpdateLectureMaterialDescription(LectureMaterial lecture, string description)
    {
        LectureMaterial.LectureMaterialsBuilder builder = LectureMaterial.LectureMaterialBuilder(CurrentUser.User);

        LectureResult result = builder.UpdateDescription(lecture, description);

        return result;
    }

    public LectureResult UpdateLectureMaterialContent(LectureMaterial lecture, string content)
    {
        LectureMaterial.LectureMaterialsBuilder builder = LectureMaterial.LectureMaterialBuilder(CurrentUser.User);

        LectureResult result = builder.UpdateContent(lecture, content);

        return result;
    }

    public SubjectResult UpdateCreditSubjectName(CreditSubject subject, string name)
    {
        CreditSubject.CreditSubjectsBuilder builder = CreditSubject.CreditSubjectBuilder(CurrentUser.User);

        SubjectResult result = builder.UpdateCreditSubjectName(subject, name);

        return result;
    }

    public SubjectResult UpdateCreditSubjectPoints(CreditSubject subject, int points)
    {
        CreditSubject.CreditSubjectsBuilder builder = CreditSubject.CreditSubjectBuilder(CurrentUser.User);

        SubjectResult result = builder.UpdateCreditPoints(subject, points);

        return result;
    }

    public SubjectResult AddLectureToCreditSubject(CreditSubject subject, LectureMaterial lecture)
    {
        CreditSubject.CreditSubjectsBuilder builder = CreditSubject.CreditSubjectBuilder(CurrentUser.User);

        SubjectResult result = builder.AddLecture(subject, lecture);

        return result;
    }

    public SubjectResult UpdateExamSubjectName(ExamSubject subject, string name)
    {
        ExamSubject.ExamSubjectsBuilder builder = ExamSubject.ExamSubjectBuilder(CurrentUser.User);

        SubjectResult result = builder.UpdateExamSubjectName(subject, name);

        return result;
    }

    public SubjectResult AddLectureToExamSubject(ExamSubject subject, LectureMaterial lecture)
    {
        ExamSubject.ExamSubjectsBuilder builder = ExamSubject.ExamSubjectBuilder(CurrentUser.User);

        SubjectResult result = builder.AddLecture(subject, lecture);

        return result;
    }

    public LectureResult GetLectureMaterial(Guid lectureId)
    {
        return _data.LectureMaterialsRepository.GetById(lectureId);
    }

    public LabWorkResult GetLabWork(Guid labWorkId)
    {
        return _data.LabWorksRepository.GetById(labWorkId);
    }

    public EducationProgramResult GetEducationProgram(Guid educationProgramId)
    {
        return _data.EducationProgramsRepository.GetById(educationProgramId);
    }

    public SubjectResult GetSubject(Guid subjectId)
    {
        return _data.SubjectsRepository.GetById(subjectId);
    }

    public UserResult GetUser(Guid userId)
    {
        return _data.UsersRepository.GetById(userId);
    }
}