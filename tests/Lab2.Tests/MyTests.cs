using Itmo.ObjectOrientedProgramming.Lab2;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Materials.Lectures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using Xunit;

namespace Lab2.Tests;

public class MyTests
{
    [Fact]

    public void ShouldBlockAccess_WhenNotOwnerTryChangeObject_ReturnsUserIsNotOwner()
    {
        var userOwner = new UniversityUser("Ivan");
        var contextUserOwner = new ContextUser(userOwner);
        var userService = new UserService(contextUserOwner);

        LectureMaterial lectureMaterial = userService.CreateLectureMaterial("OOP", "Patterns", "Builder");

        IUser userNotOwner = userService.Login("Vova");

        LectureResult actualResult = userService.UpdateLectureMaterialContent(lectureMaterial, "Factory");

        var expectedResult = new LectureResult.UserIsNotOwner(lectureMaterial);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]

    public void ShouldContainBaseIdAfterClone_WhenOneObjectCloneSecond_ReturnsSuccess()
    {
        var user = new UniversityUser("Semen");
        var contextUser = new ContextUser(user);
        var userService = new UserService(contextUser);

        LectureMaterial lectureMaterial = userService.CreateLectureMaterial("OOP", "Patterns", "Builder");

        LectureMaterial lectureMaterial2 = userService.CloneCreateLectureMaterial(lectureMaterial);

        Guid? baseIdLectureMaterial2 = lectureMaterial2.BaseId;

        Guid originIdLectureMaterial = lectureMaterial.Id;

        Assert.Equal(originIdLectureMaterial, baseIdLectureMaterial2);
    }

    [Fact]

    public void ShouldBlockCreatingSubject_WhenSumOfPointsHigherThenHundred_Returns()
    {
        var user = new UniversityUser("Roma");
        var contextUser = new ContextUser(user);
        var userService = new UserService(contextUser);

        string criteria = "Cool";
        IList<string> criterias = new List<string> { criteria };

        LectureMaterial lecture = userService.CreateLectureMaterial("OOP", "Patterns", "Builder");
        IList<LectureMaterial> lectures = new List<LectureMaterial> { lecture };

        LabWork labWork1 = userService.CreateLabWork("First", "Bad", 40, criterias);
        LabWork labWork2 = userService.CreateLabWork("Second", "Good", 40, criterias);
        IList<LabWork> labs = new List<LabWork> { labWork1, labWork2 };

        SubjectResult subjectActualResult = userService.CreateExamSubject("OOP", labs, lectures, 52);

        SubjectResult subjectExpectedResult = new SubjectResult.SubjectMustHaveHundredPoints();

        Assert.Equal(subjectExpectedResult, subjectActualResult);
    }

    [Fact]

    public void ShouldFindObject_WhenUserSearchLecture_ReturnsSuccessFound()
    {
        var user = new UniversityUser("Katya");
        var contextUser = new ContextUser(user);
        var userService = new UserService(contextUser);

        LectureMaterial lecture = userService.CreateLectureMaterial("OOP", "Patterns", "Builder");

        Guid needToFind = lecture.Id;

        LectureResult lectureFind = userService.GetLectureMaterial(needToFind);

        LectureMaterial? actualResult = lectureFind.Material;

        LectureMaterial expectedResult = lecture;

        var expectedType = new LectureResult.SuccessFound(lecture);

        Assert.Equal(expectedResult, actualResult);
        Assert.Equal(expectedType, lectureFind);
    }

    [Fact]

    public void ShouldBrokeWhenFindingObject_WhenUserSearchLecture_ReturnsFailureFound()
    {
        var user = new UniversityUser("Katya");
        var contextUser = new ContextUser(user);
        var userService = new UserService(contextUser);

        var needToFind = Guid.NewGuid();

        LectureResult lectureFindType = userService.GetLectureMaterial(needToFind);

        var expectedType = new LectureResult.FailureFound();

        Assert.Equal(expectedType, lectureFindType);
    }

    [Fact]
    public void ShouldCloneWithNewOwner_WhenObjectClonedByNewUser_ReturnsSucess()
    {
        var originalOwner = new UniversityUser("Leha");
        var contextUser = new ContextUser(originalOwner);
        var userService = new UserService(contextUser);

        LectureMaterial originalLecture = userService.CreateLectureMaterial("AAA", "BBB", "Punk");

        IUser anotherUser = userService.Login("Max");

        LectureMaterial clonedLecture = userService.CloneCreateLectureMaterial(originalLecture);

        IUser expectedOwner = anotherUser;

        IUser actualOwner = clonedLecture.Owner;

        Assert.Equal(expectedOwner, actualOwner);
    }
}