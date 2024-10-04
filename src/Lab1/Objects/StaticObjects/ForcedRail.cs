using Itmo.ObjectOrientedProgramming.Lab1.Ensures;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class ForcedRail : IPartOfPathway
{
    private readonly double _force;

    private readonly double _length;

    public ForcedRail(double length, double force)
    {
        Ensure.Positive(length, nameof(length));

        _length = length;
        _force = force;
    }

    public PassingResult TryPass(Train train)
    {
        if (!TryGiveForce(train))
        {
            return new PassingResult(false, 0);
        }

        double remainingDistance = _length - train.RemainPassedDistance;

        return TryPassDistance(remainingDistance, train);
    }

    private bool TryGiveForce(Train train)
    {
        return train.TryApplyForce(_force);
    }

    private PassingResult TryPassDistance(double distance, Train train)
    {
        var passingResult = new PassingResult(true, 0);

        if (distance <= 0)
        {
            train.UpdatePropertiesAfterForcedRail(double.Abs(distance));
            return passingResult;
        }

        double timeCounter = 0;
        while (distance > 0)
        {
            train.ChangeSpeed();
            double currentSpeed = train.Speed;
            if (currentSpeed < 0)
            {
                passingResult.IsSuccessful = false;
                passingResult.RealTime = timeCounter;
                return passingResult;
            }

            double passedDistance = currentSpeed * train.Precision;
            distance -= passedDistance;

            timeCounter++;

            if (distance <= 0)
            {
                train.UpdatePropertiesAfterForcedRail(double.Abs(distance));
                passingResult.RealTime = timeCounter;
                return passingResult;
            }
        }

        passingResult.IsSuccessful = false;

        return passingResult;
    }
}