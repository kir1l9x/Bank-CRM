using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class Route
{
    public double SpeedLimit { get; }

    public IList<IPartOfPathway> PartsOfPathway { get; } = [];

    public Route(double speedLimit)
    {
        SpeedLimit = speedLimit;
    }

    public void AddPartOfPathway(IPartOfPathway p)
    {
        this.PartsOfPathway.Add(p);
    }
}