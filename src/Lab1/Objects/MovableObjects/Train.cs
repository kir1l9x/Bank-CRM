using Itmo.ObjectOrientedProgramming.Lab1.Ensures;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

public class Train : IMovableObject
{
    private readonly double _maxForce;

    private readonly double _weight;

    public Train(double maxForce, double weight, double precision)
    {
        Ensure.Positive(maxForce, nameof(maxForce));
        Ensure.Positive(weight, nameof(weight));
        Ensure.Positive(precision, nameof(precision));

        _maxForce = maxForce;
        _weight = weight;
        Precision = precision;

        Speed = 0;
        _acceleration = 0;
        RemainPassedDistance = 0;
    }

    private double _acceleration;

    public double Speed { get; private set; }

    public double Precision { get; }

    public double RemainPassedDistance { get; private set; }

    public PassingResult TryPassWay(Route route)
    {
        var result = new PassingResult(true, 0);
        foreach (IPartOfPathway part in route.PartsOfPathway)
        {
            PassingResult currentPassingResult = part.TryPass(this);
            bool currentSuccess = currentPassingResult.IsSuccessful;
            double currentTime = currentPassingResult.RealTime;

            result.RealTime += currentTime;

            if (!currentSuccess)
            {
                result.IsSuccessful = false;
                break;
            }
        }

        if (!route.TryLetTrain(this))
        {
            result.IsSuccessful = false;
        }

        result.RealTime *= Precision;
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