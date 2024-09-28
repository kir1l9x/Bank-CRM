using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;

public class Route
{
    public Route(double speedLimit)
    {
        SpeedLimit = speedLimit;
    }

    public Collection<IPartOfPathway> PartsOfPathway { get; } = [];

    public double SpeedLimit { get; set; }
}