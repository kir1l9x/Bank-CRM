namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public interface IRepository<in T, out TResultType> where T : class
{
    void Add(T entity);

    TResultType GetById(Guid id);
}