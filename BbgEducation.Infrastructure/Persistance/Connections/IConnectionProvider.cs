using System.Data.SqlClient;

namespace BbgEducation.Infrastructure.Persistance.Connections;
public interface IConnectionProvider
{
    SqlConnection GetConnection(string connectionID = "BbgEducation");
}
