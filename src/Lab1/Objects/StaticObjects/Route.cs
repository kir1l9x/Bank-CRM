using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class Route
{
    private readonly double _speedLimit;

    private readonly List<IPartOfPathway> _partsOfPathway = [];

    public Route(double speedLimit)
    {
        _speedLimit = speedLimit;
    }

    public IReadOnlyList<IPartOfPathway> PartsOfPathway => _partsOfPathway;

    public bool TryLetTrain(Train train)
    {
        return train.Speed <= _speedLimit;
    }

    public void AddPartOfPathway(IPartOfPathway partOfPathway)
    {
        _partsOfPathway.Add(partOfPathway);
    }
}