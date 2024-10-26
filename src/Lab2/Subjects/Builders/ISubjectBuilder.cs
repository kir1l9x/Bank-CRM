namespace Itmo.ObjectOrientedProgramming.Lab2.Subjects.Builders;

public interface ISubjectBuilder
{
    SubjectResult Build();

    ISubjectBuilder SetName(string name);

    ISubjectBuilder SetPoints(int points);
}