using Itmo.ObjectOrientedProgramming.Lab2.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Terms;

namespace Itmo.ObjectOrientedProgramming.Lab2.Services;

public class TermService
{
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
}