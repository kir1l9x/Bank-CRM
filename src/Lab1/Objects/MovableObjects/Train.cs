using Itmo.ObjectOrientedProgramming.Lab1.Ensures;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

public class Train : IMovableObject
{
    public double MaxForce { get; }

    public double Weight { get; }

    public double Speed { get; set; }

    public double Acceleration { get; protected internal set; }

    public double Precision { get; }

    public double TimeCounter { get; protected internal set; }

    public double RemainPassedDistance { get; protected internal set; }

    public Train(double maxForce, double weight, double precision)
    {
        Ensure.Positive(maxForce, nameof(maxForce));
        Ensure.Positive(weight, nameof(weight));
        Ensure.Positive(precision, nameof(precision));

        MaxForce = maxForce;
        Weight = weight;
        Precision = precision;

        Speed = 0;
        Acceleration = 0;
        TimeCounter = 0;
        RemainPassedDistance = 0;
    }

    public PathwayPassingResult TryPassWay(Route route)
    {
        var result = new PathwayPassingResult(true);
        foreach (IPartOfPathway part in route.PartsOfPathway)
        {
            if (!part.TryPass(this))
            {
                result.IsSuccessful = false;
                break;
            }
        }

        if (Speed > route.SpeedLimit)
        {
            result.IsSuccessful = false;
        }

        double time = TimeCounter * Precision;
        result.RealTime = time;
        return result;
    }
}