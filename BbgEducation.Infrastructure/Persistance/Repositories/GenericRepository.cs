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
    protected abstract DynamicParameters BuildGetByIdParam(string id);

    protected abstract DynamicParameters BuildCheckNameExistsParam(string name);



    protected abstract string GetAllStoredProc { get; }

    protected abstract string GetByIDStoredProc { get; }

    protected abstract string GetNameExistsStoredProc { get; }

    protected abstract string AddUpdateStoredProc { get; }


    public async Task<T> Add(T entity) {
        using (var connection = _connectionFactory.Create()) {
            try {
                var inputParams = BuildAddUpdateParams(entity);

                var newId = await connection.ExecuteScalarAsync<string>(AddUpdateStoredProc, inputParams,
                    commandType: CommandType.StoredProcedure);

                return await GetByIdAsync(newId!);
            }
            catch (Exception ex) {
                throw new Exception($"Error adding {typeof(T).Name}: {ex.Message}");
            }

        }
    }

    public Task Delete(T entity) {
        throw new NotImplementedException();
    }

    public Task DeleteAll() {
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

    public async Task<T> GetByIdAsync(string id) {
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

    public async Task<T> Update(T entity) {
        using (var connection = _connectionFactory.Create()) {
            try {
                var inputParams = BuildAddUpdateParams(entity);

               var id = await connection.ExecuteScalarAsync<string>(AddUpdateStoredProc, inputParams,
                    commandType: CommandType.StoredProcedure);
                return await GetByIdAsync(id!);
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
