namespace Itmo.ObjectOrientedProgramming.Lab2.Ensures;

public static class Ensure
{
    public static void Positive<T>(T value, string name) where T : IComparable<T>
    {
        if (value.CompareTo(default) <= 0)
        {
            throw new ArgumentException($"{name} must be positive", name);
        }
    }

    public static void NotEmpty(string str, string name)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new ArgumentNullException(name, $"{name} cannot be null or empty");
        }
    }
}