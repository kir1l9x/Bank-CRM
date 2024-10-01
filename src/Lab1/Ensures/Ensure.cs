namespace Itmo.ObjectOrientedProgramming.Lab1.Ensures;

public static class Ensure
{
    public static void Positive(double value, string name)
    {
        if (value <= 0)
        {
            throw new ArgumentException($"{name} must be positive", name);
        }
    }

    public static void Positive(int value, string name)
    {
        if (value <= 0)
        {
            throw new ArgumentException($"{name} must be positive", name);
        }
    }

    public static void NonNegative(int value, string name)
    {
        if (value < 0)
        {
            throw new ArgumentException($"{name} must be non-negative", name);
        }
    }

    public static void NonNegative(double value, string name)
    {
        if (value < 0)
        {
            throw new ArgumentException($"{name} must be non-negative", name);
        }
    }
}