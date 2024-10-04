namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public class PassingResult
{
    public PassingResult(bool isSuccessful, double realTime)
    {
        IsSuccessful = isSuccessful;
        RealTime = realTime;
    }

    public bool IsSuccessful { get; protected internal set; }

    public double RealTime { get; protected internal set; }

    public override bool Equals(object? obj)
    {
        if (obj is PassingResult other)
        {
            return IsSuccessful == other.IsSuccessful && RealTime == other.RealTime;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IsSuccessful, RealTime);
    }
}