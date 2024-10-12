namespace Itmo.ObjectOrientedProgramming.Lab1.Ensures;

public static class Ensure
{
    public static void Positive<T>(T value, string name) where T : IComparable<T>
    {
        if (value.CompareTo(default) <= 0)
        {
            throw new ArgumentException($"{name} must be positive", name);
        }
    }

    public static void NonNegative<T>(T value, string name) where T : IComparable<T>
    {
        if (value.CompareTo(default) < 0)
        {
            throw new ArgumentException($"{name} must be non-negative", name);
        }
    }
}