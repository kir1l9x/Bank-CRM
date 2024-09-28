using Itmo.ObjectOrientedProgramming.Lab1.Objects.MovableObjects;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface IPartOfPathway
{
    public bool TryPass(Train train);
}