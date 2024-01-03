namespace BbgEducation.Infrastructure.Persistance.Repositories;
public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(bool? includeInactive = false);
    Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, T, U>(string splitOn, Func<TFirst, TSecond, T> map, U parameters);
    Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, TThird, T, U>(string splitOnFirst, string splitOnSecond, Func<TFirst, TSecond, TThird, T> map, U parameters);
    Task<bool> CheckNameExistsAsync(string name);

    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task Delete(T entity);
    Task DeleteAll();
}
