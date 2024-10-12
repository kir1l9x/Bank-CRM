using Itmo.ObjectOrientedProgramming.Lab1.Ensures;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class CommonRail : IPartOfPathway
{
    private readonly double _length;

    public CommonRail(double length)
    {
        Ensure.Positive(length, nameof(length));

        _length = length;
    }

    public PassingResult TryPass(Train train)
    {
        double remainingDistance = _length - train.RemainPassedDistance;

        return TryPassDistance(remainingDistance, train);
    }

    private PassingResult TryPassDistance(double distance, Train train)
    {
        if (distance <= 0)
        {
            train.UpdatePropertiesAfterCommonRail(double.Abs(distance));
            return new PassingResult.Success(0);
        }

        double timeCounter = 0;
        double currentSpeed = train.Speed;
        while (distance > 0)
        {
            if (train.Speed <= 0)
            {
                return new PassingResult.SpeedIsNonPositiveOnCommonRail(0);
            }

            double passedDistance = currentSpeed * train.Precision;
            distance -= passedDistance;

            timeCounter++;

            if (distance <= 0)
            {
                train.UpdatePropertiesAfterCommonRail(double.Abs(distance));
                return new PassingResult.Success(timeCounter);
            }
        }

        return new PassingResult.Success(0);
    }
}