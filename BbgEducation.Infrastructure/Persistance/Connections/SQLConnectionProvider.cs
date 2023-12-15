using BbgEducation.Infrastructure.Persistance.Connections;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess.DbAccess;
public class SQLConnectionProvider : IConnectionProvider
{
    private readonly IConfiguration _config;

    public SQLConnectionProvider(IConfiguration config) {
        _config = config;
    }
    public SqlConnection GetConnection(string connectionID = "BbgEducation") {        
        var cstr = _config.GetConnectionString(connectionID);        
        return new SqlConnection(cstr);
    }
}
