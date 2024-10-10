using Itmo.ObjectOrientedProgramming.Lab1.Ensures;
using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class Station : IPartOfPathway
{
    private readonly double _timeToLet;

    private readonly double _speedLimit;

    public Station(double speedLimit, int peopleBandwidth, int peopleAmount)
    {
        Ensure.NonNegative(speedLimit, nameof(speedLimit));
        Ensure.NonNegative(peopleAmount, nameof(peopleAmount));
        Ensure.NonNegative(PeopleBandwidth, nameof(PeopleBandwidth));

        _speedLimit = speedLimit;
        PeopleAmount = peopleAmount;
        PeopleBandwidth = peopleBandwidth;

        _timeToLet = Convert.ToDouble(peopleAmount / peopleBandwidth);
    }

    public int PeopleAmount { get; set; }

    public int PeopleBandwidth { get; init; }

    public PassingResult TryPass(Train train)
    {
        if (!TryLetTrain(train.Speed))
        {
            return new PassingResult.TooHighSpeedForStation(0);
        }

        train.UpdatePropertiesAfterStation();
        double timeToLet = _timeToLet / train.Precision;

        return new PassingResult.Success(timeToLet);
    }

    private bool TryLetTrain(double speed)
    {
        if (speed > _speedLimit)
        {
            return false;
        }

        return true;
    }
}