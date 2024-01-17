using BbgEducation.Application.Common.Errors;
using BbgEducation.Infrastructure.Persistance.Common;
using BbgEducation.Infrastructure.Persistance.Connections;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

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
            catch (SqlException sx) {
                //TODO:   add logging and log full stack trace, etc.
                throw SqlExceptionHelper.ToDBException(sx);
            }
            catch (Exception ex) {
                throw new Exception($"Error adding {typeof(T).Name}: {ex.Message}");
            }

        }
    }

    public void Update(T entity) {
        using (var connection = _connectionFactory.Create()) {
            try {
                var inputParams = BuildAddUpdateParams(entity);

                var id = connection.ExecuteScalar<int>(AddUpdateStoredProc, inputParams,
                     commandType: CommandType.StoredProcedure);
            }
            catch (SqlException sx) {
                //TODO:   add logging and log full stack trace, etc.
                throw SqlExceptionHelper.ToDBException(sx);
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
        }

    }

    public void Delete(T entity) {
        throw new NotImplementedException();
    }

    public void DeleteAll() {
        throw new NotImplementedException();
    }


    public async Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, T, U>(string splitOn,
        Func<TFirst, TSecond, T> map, U parameters, CancellationToken cancellationToken) {
        
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                var commandDef = new CommandDefinition(GetAllStoredProc, parameters, commandType: CommandType.StoredProcedure,
                    cancellationToken: cancellationToken);
                
                data = await connection.QueryAsync<TFirst, TSecond, T>(commandDef, map, splitOn: $"{splitOn}");
                //data = await connection.QueryAsync<TFirst, TSecond, T>(GetAllStoredProc,
                //    map,
                //    splitOn: $"{splitOn}",
                //    param: parameters,
                //    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException sx) {
                //TODO:   add logging and log full stack trace, etc.
                throw SqlExceptionHelper.ToDBException(sx);
            }
            catch(TaskCanceledException cx) {
                //log but don't throw
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync<TFirst, TSecond, TThird, T, U>(string splitOnFirst, string splitOnSecond, 
        Func<TFirst, TSecond, TThird, T> map, U parameters, CancellationToken cancellationToken) {
       
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                var commandDef = new CommandDefinition(GetAllStoredProc, parameters, commandType: CommandType.StoredProcedure,
                    cancellationToken: cancellationToken);
                data = await connection.QueryAsync<TFirst, TSecond, TThird, T>(commandDef, map, splitOn: $"{splitOnFirst}, {splitOnSecond}");
                //data = await connection.QueryAsync<TFirst, TSecond, TThird, T>(GetAllStoredProc,
                //    map,
                //    splitOn: $"{splitOnFirst}, {splitOnSecond}",
                //    param: parameters,
                //    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException sx) {
                //TODO:   add logging and log full stack trace, etc.
                throw SqlExceptionHelper.ToDBException(sx);
            }
            catch (TaskCanceledException cx) {
                //log but don't throw
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data;
        }
    }  


    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken, bool? includeInactive = null) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                var commandDef = new CommandDefinition(
                    GetAllStoredProc,
                    includeInactive.HasValue ? new { IncludeInactive = includeInactive } : new { },
                    commandType: CommandType.StoredProcedure,
                    cancellationToken: cancellationToken);
                
                data = await connection.QueryAsync<T>(commandDef);                

            }
            catch (SqlException sx) {
                //TODO:   add logging and log full stack trace, etc.
                throw SqlExceptionHelper.ToDBException(sx);
            }
            catch (TaskCanceledException cx) {
                //log but don't throw
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data;
        }
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                var commandDef = new CommandDefinition(
                    GetByIDStoredProc,
                    BuildGetByIdParam(id),
                    commandType: CommandType.StoredProcedure,
                    cancellationToken: cancellationToken);

                data = await connection.QueryAsync<T>(GetByIDStoredProc,
                    param: BuildGetByIdParam(id),
                    commandType: CommandType.StoredProcedure
                 );
            }
            catch (SqlException sx) {
                //TODO:   add logging and log full stack trace, etc.
                throw SqlExceptionHelper.ToDBException(sx);
            }
            catch (TaskCanceledException cx) {
                //log but don't throw
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data.FirstOrDefault()!;
        }
    }

    public async Task<T> GetByIdAsync<TFirst, TSecond, T>(int id, string splitOn, Func<TFirst, TSecond, T> map, 
        CancellationToken cancellationToken) {

        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                var commandDef = new CommandDefinition(
                   GetByIDStoredProc,
                   BuildGetByIdParam(id),
                   commandType: CommandType.StoredProcedure,
                   cancellationToken: cancellationToken);
                data = await connection.QueryAsync<TFirst, TSecond, T>(commandDef, map, splitOn: $"{splitOn}");
            }
            catch (SqlException sx) {
                //TODO:   add logging and log full stack trace, etc.
                throw SqlExceptionHelper.ToDBException(sx);
            }
            catch (TaskCanceledException cx) {
                //log but don't throw
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data.FirstOrDefault()!;
        }
    }

    public async Task<T> GetByIdAsync<TFirst, TSecond, TThird, T>(int id, string splitOnFirst, string splitOnSecond, 
        Func<TFirst, TSecond, TThird, T> map, CancellationToken cancellationToken) {
        IEnumerable<T> data = new List<T>();

        using (var connection = _connectionFactory.Create()) {
            try {
                var commandDef = new CommandDefinition(
                   GetByIDStoredProc,
                   BuildGetByIdParam(id),
                   commandType: CommandType.StoredProcedure,
                   cancellationToken: cancellationToken);
                data = await connection.QueryAsync<TFirst, TSecond, TThird, T>(commandDef, map, splitOn: $"{splitOnFirst}, {splitOnSecond}");
            }
            catch (SqlException sx) {
                //TODO:   add logging and log full stack trace, etc.
                throw SqlExceptionHelper.ToDBException(sx);
            }
            catch (TaskCanceledException cx) {
                //log but don't throw
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }
            return data.FirstOrDefault()!;
        }
    }
    public async Task<bool> CheckNameExistsAsync(string name, CancellationToken cancellationToken) {

        using (var connection = _connectionFactory.Create()) {
            try {
                var commandDef = new CommandDefinition(
                    GetNameExistsStoredProc,
                    BuildCheckNameExistsParam(name),
                    commandType: CommandType.StoredProcedure,
                    cancellationToken: cancellationToken);

                var exists = await connection.ExecuteScalarAsync<bool>(commandDef);
                return exists;
            }
            catch (SqlException sx) {
                //TODO:   add logging and log full stack trace, etc.
                throw SqlExceptionHelper.ToDBException(sx);
            }
            catch (TaskCanceledException cx) {
                //log but don't throw
                return false;  //not exactly sure what to do here...
            }
            catch (Exception ex) {
                throw new Exception($"Error fetching {typeof(T).Name}: {ex.Message}");
            }

        }
    }
}
