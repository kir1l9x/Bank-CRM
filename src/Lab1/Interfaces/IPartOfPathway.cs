using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface IPartOfPathway
{
    bool TryPass(Train train);
}