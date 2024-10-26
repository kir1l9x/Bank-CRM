using Itmo.ObjectOrientedProgramming.Lab2.Ensures;
using Itmo.ObjectOrientedProgramming.Lab2.Subjects;

namespace Itmo.ObjectOrientedProgramming.Lab2.Terms;

public class Term : ITerm
{
    private Term(int number, IReadOnlyList<ISubject> subjects)
    {
        Number = number;
        Subjects = subjects;
    }

    public static TermsBuilder TermBuilder => new TermsBuilder();

    public IReadOnlyList<ISubject> Subjects { get; } = [];

    public int Number { get; }

    public class TermsBuilder
    {
        private readonly List<ISubject> _subjects = [];

        private int _number;

        public TermsBuilder SetTermNumber(int number)
        {
            Ensure.Positive(number, nameof(number));

            _number = number;
            return this;
        }

        public TermsBuilder AddSubject(ISubject subject)
        {
            _subjects.Add(subject);
            return this;
        }

        public ITerm Build()
        {
            return new Term(_number, _subjects.AsReadOnly());
        }
    }
}