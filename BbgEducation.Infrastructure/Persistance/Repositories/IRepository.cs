namespace BbgEducation.Infrastructure.Persistance.Repositories;
public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id,CancellationToken cancellationToken);
    Task<T> GetByIdAsync<TFirst, TSecond, T>(int id, string splitOn, Func<TFirst, TSecond, T> map,
       CancellationToken cancellationToken);
    Task<T> GetByIdAsync<TFirst, TSecond, TThird, T>(int id, string splitOnFirst, string splitOnSecond,
        Func<TFirst, TSecond, TThird, T> map, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken, bool? includeInactive = false);
    Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, T, U>(string splitOn, Func<TFirst, TSecond, T> map, U parameters, 
        CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, TThird, T, U>(string splitOnFirst, string splitOnSecond, 
        Func<TFirst, TSecond, TThird, T> map, U parameters, CancellationToken cancellationToken);
    Task<bool> CheckNameExistsAsync(string name, CancellationToken cancellationToken);

    int Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void DeleteAll();
}
