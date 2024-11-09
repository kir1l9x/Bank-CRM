namespace Itmo.ObjectOrientedProgramming.Lab3.Priorities;

public abstract record PriorityLevel
{
    public int? PriorityNumber { get; private set; }

    public sealed record Max : PriorityLevel
    {
        public Max()
        {
            PriorityNumber = 4;
        }
    }

    public sealed record High : PriorityLevel
    {
        public High()
        {
            PriorityNumber = 3;
        }
    }

    public sealed record Mid : PriorityLevel
    {
        public Mid()
        {
            PriorityNumber = 2;
        }
    }

    public sealed record Low : PriorityLevel
    {
        public Low()
        {
            PriorityNumber = 1;
        }
    }
}