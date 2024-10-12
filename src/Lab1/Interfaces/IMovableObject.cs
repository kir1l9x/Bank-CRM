using Itmo.ObjectOrientedProgramming.Lab1.Objects.StaticObjects;
using Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface IMovableObject
{
    double Speed { get; }

    PassingResult TryPassWay(Route route);
}