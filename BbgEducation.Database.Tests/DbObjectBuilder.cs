using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Database.Tests;
public static class DbObjectBuilder
{
    public static void Build(string connectionString) {
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            Server server = new Server(new ServerConnection(connection));
            //ToDo:  use script that lists files in proper sequence
            RunScripts(server, "Scripts/DropObjects");

            
            RunScripts(server, "Scripts/Tables");
            RunScripts(server, "Scripts/Views");
            RunScripts(server, "Scripts/StoredProcedures");

            RunScripts(server, "Scripts/SeedData");
        }
    }


    private static void RunScripts(Server dbServer, String scriptDirectory) {
        List<String> scripts = Directory.EnumerateFiles(
               scriptDirectory, "*.sql", SearchOption.AllDirectories)
               .ToList();

        scripts.ForEach(script => {
            dbServer.ConnectionContext.ExecuteNonQuery(File.ReadAllText(script));
        });
    }
}
