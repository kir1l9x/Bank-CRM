namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.SpecificEducationPrograms.AdditionalEducationPrograms;

public interface IAdditionalEducationProgram : IEducationProgram
{
    string ProvidingCompanyName { get; }
}