using Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services;

public class SubjectService(IUser user, ContextData contextData)
{
    public SubjectResult CreateCreditSubject(string subjectName, IList<LabWork> labWorks, IList<LectureMaterial> lectures, int points)
    {
        CreditSubject.CreditSubjectsBuilder builder = CreditSubject.CreditSubjectBuilder(user);

        builder.SetName(subjectName).SetPoints(points);

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
                contextData.SubjectsRepository.Add(creditSubject.Subject);
            }
        }

        return creditSubject;
    }

    public SubjectResult CreateExamSubject(string subjectName, IList<LabWork> labWorks, IList<LectureMaterial> lectures, int points)
    {
        ExamSubject.ExamSubjectsBuilder builder = ExamSubject.ExamSubjectBuilder(user);

        builder.SetName(subjectName).SetPoints(points);

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
                contextData.SubjectsRepository.Add(examSubject.Subject);
            }
        }

        return examSubject;
    }

    public ISubject CloneCreateCreditSubject(CreditSubject creditSubject)
    {
        ISubject result = creditSubject.Clone(user);

        contextData.SubjectsRepository.Add(result);

        return result;
    }

    public ISubject CloneCreateExamSubject(ExamSubject examSubject)
    {
        ISubject result = examSubject.Clone(user);

        contextData.SubjectsRepository.Add(result);

        return result;
    }

    public SubjectResult UpdateCreditSubjectName(CreditSubject subject, string name)
    {
        CreditSubject.CreditSubjectsBuilder builder = CreditSubject.CreditSubjectBuilder(user);

        SubjectResult result = builder.UpdateCreditSubjectName(subject, name);

        return result;
    }

    public SubjectResult UpdateCreditSubjectPoints(CreditSubject subject, int points)
    {
        CreditSubject.CreditSubjectsBuilder builder = CreditSubject.CreditSubjectBuilder(user);

        SubjectResult result = builder.UpdateCreditPoints(subject, points);

        return result;
    }

    public SubjectResult AddLectureToCreditSubject(CreditSubject subject, LectureMaterial lecture)
    {
        CreditSubject.CreditSubjectsBuilder builder = CreditSubject.CreditSubjectBuilder(user);

        SubjectResult result = builder.AddLecture(subject, lecture);

        return result;
    }

    public SubjectResult UpdateExamSubjectName(ExamSubject subject, string name)
    {
        ExamSubject.ExamSubjectsBuilder builder = ExamSubject.ExamSubjectBuilder(user);

        SubjectResult result = builder.UpdateExamSubjectName(subject, name);

        return result;
    }

    public SubjectResult AddLectureToExamSubject(ExamSubject subject, LectureMaterial lecture)
    {
        ExamSubject.ExamSubjectsBuilder builder = ExamSubject.ExamSubjectBuilder(user);

        SubjectResult result = builder.AddLecture(subject, lecture);

        return result;
    }

    public SubjectResult GetSubject(Guid subjectId)
    {
        return contextData.SubjectsRepository.GetById(subjectId);
    }
}