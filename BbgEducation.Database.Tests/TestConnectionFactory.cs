using BbgEducation.Infrastructure.Persistance.Connections;
using System.Data;
using System.Data.SqlClient;


namespace BbgEducation.Database.Tests;
public class TestConnectionFactory : ISQLConnectionFactory
{
    private readonly string _connectionString;

    public TestConnectionFactory(string connectionString) {
        _connectionString = connectionString;
    }
    public IDbConnection Create() {
        return new SqlConnection(_connectionString);
    }
}
