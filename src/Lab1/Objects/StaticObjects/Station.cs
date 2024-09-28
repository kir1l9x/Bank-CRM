using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class Station : IPartOfPathway
{
    public Station(double speedLimit, int peopleBandwidth, int peopleAmount)
    {
        if (speedLimit < 0)
        {
            throw new ArgumentException("Speed limit cannot be negative.");
        }

        if (peopleAmount < 0)
        {
            throw new ArgumentException("People amount cannot be negative.");
        }

        if (peopleBandwidth < 0)
        {
            throw new ArgumentException("People bandwidth cannot be negative.");
        }

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

    public int PeopleAmount { get; set; }

    public int PeopleBandwidth { get; set; }

    private double TimeToLet { get; }

    private double SpeedLimit { get; }
}