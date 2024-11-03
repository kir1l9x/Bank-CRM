namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public abstract class BaseRepository<T, TResultType> : IRepository<T, TResultType> where T : class
{
    protected IList<T> Items { get; } = [];

    public void Add(T entity)
    {
        Items.Add(entity);
    }

    public abstract TResultType GetById(Guid id);
}