using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class Route
{
    private readonly double _speedLimit;

    public Route(double speedLimit)
    {
        _speedLimit = speedLimit;
    }

    public IList<IPartOfPathway> PartsOfPathway { get; } = [];

    public bool TryLetTrain(Train train)
    {
        if (train.Speed > _speedLimit)
        {
            return false;
        }

        return true;
    }
}