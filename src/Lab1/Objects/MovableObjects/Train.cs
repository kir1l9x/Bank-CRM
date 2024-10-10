using Itmo.ObjectOrientedProgramming.Lab1.Ensures;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

public class Train : IMovableObject
{
    private readonly double _maxForce;

    private readonly double _weight;

    private double _acceleration;

    public Train(double maxForce, double weight, double precision)
    {
        Ensure.Positive(maxForce, nameof(maxForce));
        Ensure.Positive(weight, nameof(weight));
        Ensure.Positive(precision, nameof(precision));

        Speed = 0;
        _acceleration = 0;
        RemainPassedDistance = 0;

        _maxForce = maxForce;
        _weight = weight;
        Precision = precision;
    }

    public double Speed { get; private set; }

    public double Precision { get; }

    public double RemainPassedDistance { get; private set; }

    public PassingResult TryPassWay(Route route)
    {
        var result = new PassingResult.Success(0);
        foreach (IPartOfPathway part in route.PartsOfPathway)
        {
            PassingResult currentPassingResult = part.TryPass(this);
            double currentTime = currentPassingResult.Time;

            result.Time += currentTime;

            if (currentPassingResult is not PassingResult.Success)
            {
                currentPassingResult.Time = result.Time;
                currentPassingResult.Time *= Precision;
                return currentPassingResult;
            }
        }

        if (!route.TryLetTrain(this))
        {
            result.Time *= Precision;
            return new PassingResult.TooHighSpeedForRoute(result.Time);
        }

        result.Time *= Precision;
        return result;
    }

    public bool TryApplyForce(double force)
    {
        if (double.Abs(force) > _maxForce)
        {
            return false;
        }

        double additionalAcceleration = force / _weight;
        _acceleration += additionalAcceleration;

        return true;
    }

    public void ChangeSpeed()
    {
        Speed += _acceleration * Precision;
    }

    public void UpdatePropertiesAfterForcedRail(double extraPassedDistance)
    {
        _acceleration = 0;
        RemainPassedDistance = extraPassedDistance;
    }

    public void UpdatePropertiesAfterCommonRail(double extraPassedDistance)
    {
        RemainPassedDistance = extraPassedDistance;
    }

    public void UpdatePropertiesAfterStation()
    {
        RemainPassedDistance = 0;
    }
}