using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Infrastructure.Persistance.Connections;
internal sealed class SQLConnectionFactory : ISQLConnectionFactory
{
    private readonly string _connectionString;

    public SQLConnectionFactory(IConfiguration config) {
        _connectionString = config.GetConnectionString("BbgEducation") ??
            throw new ApplicationException("Database BbgEducation connection string is missing");
    }
    public IDbConnection Create() {
        return new SqlConnection(_connectionString);
    }
}
