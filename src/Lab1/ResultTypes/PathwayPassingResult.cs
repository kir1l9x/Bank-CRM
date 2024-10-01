namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public class PathwayPassingResult
{
    public bool IsSuccessful { get; protected internal set; }

    public double RealTime { get; protected internal set; }

    public PathwayPassingResult(bool isSuccessful, double realTime)
    {
        IsSuccessful = isSuccessful;
        RealTime = realTime;
    }

    public PathwayPassingResult(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }

    public override bool Equals(object? obj)
    {
        if (obj is PathwayPassingResult other)
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