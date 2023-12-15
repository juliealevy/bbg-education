using BbgEducation.Infrastructure.Persistance.Connections;
using Dapper;
using System.Data;

namespace BbgEducation.Infrastructure.Persistance.Repositories;
public abstract class GenericRepository<T> : IRepository<T>
{
    private readonly IConnectionProvider _connectionProvider;

    protected GenericRepository(IConnectionProvider connectionProvider) {
        _connectionProvider = connectionProvider;
    }

    protected abstract DynamicParameters BuildAddUpdateParams(T entity);
    protected abstract DynamicParameters BuildGetByIdParam(int id);

    protected abstract string GetAllStoredProc { get; }

    protected abstract string GetByIDStoredProc { get; }

    protected abstract string AddUpdateStoredProc { get; }


    public async Task Add(T entity) {
        using (var connection = _connectionProvider.GetConnection()) {
            try {
                var inputParams = BuildAddUpdateParams(entity);

                await connection.ExecuteAsync(AddUpdateStoredProc, inputParams,
                    commandType: CommandType.StoredProcedure);
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


    public async Task<IEnumerable<T>> GetAll<TFirst, TSecond, T, U>(string splitOn, Func<TFirst, TSecond, T> map, U parameters) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionProvider.GetConnection()) {
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

    public async Task<IEnumerable<T>> GetAll<TFirst, TSecond, TThird, T, U>(string splitOnFirst, string splitOnSecond, Func<TFirst, TSecond, TThird, T> map, U parameters) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionProvider.GetConnection()) {
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


    public async Task<IEnumerable<T>> GetAll(bool? includeInactive = null) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionProvider.GetConnection()) {
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

    public async Task<T> GetById(int id) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionProvider.GetConnection()) {
            try {
                data = await connection.QueryAsync<T>(GetByIDStoredProc,
                    param: BuildGetByIdParam(id),
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data.FirstOrDefault();
        }
    }

    public async Task Update(T entity) {
        using (var connection = _connectionProvider.GetConnection()) {
            try {
                var inputParams = BuildAddUpdateParams(entity);

                await connection.ExecuteAsync(AddUpdateStoredProc, inputParams,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) {
                throw new Exception($"Error updating {typeof(T).Name}: {ex.Message}");
            }

        }
    }
}
