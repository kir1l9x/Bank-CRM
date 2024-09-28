using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

public class Train : IMovableObject
{
    public Train(double maxForce, double weight, double precision)
    {
        if (maxForce <= 0)
        {
            throw new ArgumentException("Max force must be greater than zero.");
        }

        if (weight <= 0)
        {
            throw new ArgumentException("Weight must be greater than zero.");
        }

        if (precision <= 0)
        {
            throw new ArgumentException("Precision must be greater than zero.");
        }

        MaxForce = maxForce;
        Weight = weight;
        Precision = precision;

        Speed = 0;
        Acceleration = 0;
        TimeCounter = 0;
        RemainPassedDistance = 0;
    }

    public WayPassingResult TryPassWay(Route route)
    {
        var result = new WayPassingResult(true);
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

    public double MaxForce { get; }

    public double Weight { get; }

    public double Speed { get; set; }

    public double Acceleration { get; protected internal set; }

    public double Precision { get; }

    public double TimeCounter { get; protected internal set; }

    public double RemainPassedDistance { get; protected internal set; }
}