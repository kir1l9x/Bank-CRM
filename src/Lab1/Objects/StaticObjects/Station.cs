using Itmo.ObjectOrientedProgramming.Lab1.Ensures;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class Station : IPartOfPathway
{
    public int PeopleAmount { get; set; }

    public int PeopleBandwidth { get; init; }

    private double TimeToLet { get; }

    private double SpeedLimit { get; }

    public Station(double speedLimit, int peopleBandwidth, int peopleAmount)
    {
        Ensure.NonNegative(speedLimit, nameof(speedLimit));
        Ensure.NonNegative(peopleAmount, nameof(peopleAmount));
        Ensure.NonNegative(PeopleBandwidth, nameof(PeopleBandwidth));

        SpeedLimit = speedLimit;
        PeopleAmount = peopleAmount;
        PeopleBandwidth = peopleBandwidth;

        TimeToLet = Convert.ToDouble(peopleAmount / peopleBandwidth);
    }

    public bool TryPass(Train train)
    {
        if (train.Speed > SpeedLimit)
        {
            return false;
        }

        train.RemainPassedDistance = 0;

        double timeToLet = TimeToLet / train.Precision;

        train.TimeCounter += timeToLet;

        return true;
    }
}