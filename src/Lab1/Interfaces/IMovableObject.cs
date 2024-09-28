using Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface IMovableObject
{
    public WayPassingResult TryPassWay(Route route);

    public double Speed { get; protected set; }
}