namespace BbgEducation.Infrastructure.Persistance.Repositories;
public interface IRepository<T>
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll(bool? includeInactive = false);

    Task<IEnumerable<T>> GetAll<TFirst, TSecond, T, U>(string splitOn, Func<TFirst, TSecond, T> map, U parameters);

    Task<IEnumerable<T>> GetAll<TFirst, TSecond, TThird, T, U>(string splitOnFirst, string splitOnSecond, Func<TFirst, TSecond, TThird, T> map, U parameters);

    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task DeleteAll();
}
