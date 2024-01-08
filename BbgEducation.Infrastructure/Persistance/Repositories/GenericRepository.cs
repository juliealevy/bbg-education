using BbgEducation.Infrastructure.Persistance.Connections;
using Dapper;
using System.Data;

namespace BbgEducation.Infrastructure.Persistance.Repositories;
public abstract class GenericRepository<T> : IRepository<T>
{
    private readonly ISQLConnectionFactory _connectionFactory;

    protected GenericRepository(ISQLConnectionFactory sqlConnectionFactory) {
        _connectionFactory = sqlConnectionFactory;
    }

    protected abstract DynamicParameters BuildAddUpdateParams(T entity);
    protected abstract DynamicParameters BuildGetByIdParam(int id);

    protected abstract DynamicParameters BuildCheckNameExistsParam(string name);



    protected abstract string GetAllStoredProc { get; }

    protected abstract string GetByIDStoredProc { get; }

    protected abstract string GetNameExistsStoredProc { get; }

    protected abstract string AddUpdateStoredProc { get; }

    public int Add(T entity) {
        using (var connection = _connectionFactory.Create()) {
            try {
                var inputParams = BuildAddUpdateParams(entity);

                var newId = connection.ExecuteScalar<int>(AddUpdateStoredProc, inputParams,
                    commandType: CommandType.StoredProcedure);

                return newId;
            }
            catch (Exception ex) {
                throw new Exception($"Error adding {typeof(T).Name}: {ex.Message}");
            }

        }
    }

    public void Delete(T entity) {
        throw new NotImplementedException();
    }

    public void DeleteAll() {
        throw new NotImplementedException();
    }


    public async Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, T, U>(string splitOn, Func<TFirst, TSecond, T> map, U parameters) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                data = await connection.QueryAsync<TFirst, TSecond, T>(GetAllStoredProc,
                    map,
                    splitOn: $"{splitOn}",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, TThird, T, U>(string splitOnFirst, string splitOnSecond, Func<TFirst, TSecond, TThird, T> map, U parameters) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                data = await connection.QueryAsync<TFirst, TSecond, TThird, T>(GetAllStoredProc,
                    map,
                    splitOn: $"{splitOnFirst}, {splitOnSecond}",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data;
        }
    }  


    public async Task<IEnumerable<T>> GetAllAsync(bool? includeInactive = null) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {

                data = await connection.QueryAsync<T>(GetAllStoredProc,
                    param: includeInactive.HasValue ? new { IncludeInactive = includeInactive } : new { },
                    commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data;
        }
    }

    public async Task<T> GetByIdAsync(int id) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                data = await connection.QueryAsync<T>(GetByIDStoredProc,
                    param: BuildGetByIdParam(id),
                    commandType: CommandType.StoredProcedure
                 );
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data.FirstOrDefault()!;
        }
    }

    public async Task<T> GetByIdAsync<TFirst, TSecond, T>(int id, string splitOn, Func<TFirst, TSecond, T> map) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                data = await connection.QueryAsync<TFirst, TSecond, T>(GetByIDStoredProc,
                    map,
                    splitOn: $"{splitOn}",
                    param: BuildGetByIdParam(id),
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data.FirstOrDefault()!;
        }
    }

    public async Task<T> GetByIdAsync<TFirst, TSecond, TThird, T>(int id, string splitOnFirst, string splitOnSecond, Func<TFirst, TSecond, TThird, T> map) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                data = await connection.QueryAsync<TFirst, TSecond, TThird, T>(GetByIDStoredProc,
                    map,
                    splitOn: $"{splitOnFirst}, {splitOnSecond}",
                    param: BuildGetByIdParam(id),
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data.FirstOrDefault()!;
        }
    }
    public void Update(T entity) {
        using (var connection = _connectionFactory.Create()) {
            try {
                var inputParams = BuildAddUpdateParams(entity);

               var id = connection.ExecuteScalarAsync<int>(AddUpdateStoredProc, inputParams,
                    commandType: CommandType.StoredProcedure);                
            }
            catch (Exception ex) {
                throw new Exception($"Error updating {typeof(T).Name}: {ex.Message}");
            }

        }
    }

    public async Task<bool> CheckNameExistsAsync(string name) {

        using (var connection = _connectionFactory.Create()) {
            try {
                var exists = await connection.ExecuteScalarAsync<bool>(GetNameExistsStoredProc,
                    param: BuildCheckNameExistsParam(name),
                    commandType: CommandType.StoredProcedure
                 );
                return exists;
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            
        }
    }
}
