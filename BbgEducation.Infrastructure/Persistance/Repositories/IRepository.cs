namespace BbgEducation.Infrastructure.Persistance.Repositories;
public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(bool? includeInactive = false);
    Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, T, U>(string splitOn, Func<TFirst, TSecond, T> map, U parameters);
    Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, TThird, T, U>(string splitOnFirst, string splitOnSecond, Func<TFirst, TSecond, TThird, T> map, U parameters);
    Task<bool> CheckNameExistsAsync(string name);

    int Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void DeleteAll();
}
