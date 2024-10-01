using Itmo.ObjectOrientedProgramming.Lab1.Ensures;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class CommonRail : IPartOfPathway
{
    private readonly double _length;

    public CommonRail(double length)
    {
        Ensure.Positive(length, nameof(length));

        _length = length;
    }

    public bool TryPass(Train train)
    {
        double remainingDistance = _length - train.RemainPassedDistance;

        return TryPassDistance(remainingDistance, train);
    }

    private bool TryPassDistance(double distance, Train train)
    {
        if (distance <= 0)
        {
            train.RemainPassedDistance = double.Abs(distance);
            return true;
        }

        double currentSpeed = train.Speed;
        while (distance > 0)
        {
            if (train.Speed == 0)
            {
                return false;
            }

            double passedDistance = currentSpeed * train.Precision;
            distance -= passedDistance;

            train.Speed = currentSpeed;
            train.TimeCounter++;

            if (distance <= 0)
            {
                train.RemainPassedDistance = double.Abs(distance);
                return true;
            }
        }

        return false;
    }
}