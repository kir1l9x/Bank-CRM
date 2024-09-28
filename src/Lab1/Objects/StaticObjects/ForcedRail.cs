using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class ForcedRail : IPartOfPathway
{
    public ForcedRail(double length, double force)
    {
        this.Length = length;
        Force = force;
    }

    public bool TryPass(Train train)
    {
        if (!TryGiveForce(train))
        {
            return false;
        }

        double remainingDistance = Length - train.RemainPassedDistance;

        return TryPassDistance(remainingDistance, train);
    }

    private bool TryGiveForce(Train train)
    {
        if (double.Abs(Force) > double.Abs(train.MaxForce))
        {
            return false;
        }

        double additionalAcceleration = Force / train.Weight;
        train.Acceleration += additionalAcceleration;

        return true;
    }

    private double Length { get; init; }

    private bool TryPassDistance(double distance, Train train)
    {
        if (distance <= 0)
        {
            train.RemainPassedDistance = double.Abs(distance);
            return true;
        }

        while (distance > 0)
        {
            double currentSpeed = train.Speed + (train.Acceleration * train.Precision);
            if (currentSpeed < 0)
            {
                return false;

                // throw new InvalidOperationException("Speed is negative");
            }

            double passedDistance = currentSpeed * train.Precision;
            distance -= passedDistance;

            train.Speed = currentSpeed;
            train.TimeCounter++;

            if (distance <= 0)
            {
                train.RemainPassedDistance = double.Abs(distance);
                train.Acceleration = 0;
                return true;
            }
        }

        return false;
    }

    private double Force { get; }
}