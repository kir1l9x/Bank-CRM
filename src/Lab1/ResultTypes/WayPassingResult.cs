namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public class WayPassingResult
{
    public WayPassingResult(bool isSuccessful, double realTime)
    {
        IsSuccessful = isSuccessful;
        RealTime = realTime;
    }

    public WayPassingResult(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }

    public bool IsSuccessful { get; protected internal set; }

    public double RealTime { get; protected internal set; }

    public override bool Equals(object? obj)
    {
        if (obj is WayPassingResult other)
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