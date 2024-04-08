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

    private static string DIR_STOREDPROCS = "Scripts/StoredProcedures";
    private static string DIR_VIEWS = "Scripts/Views";
    private static string DIR_TABLES = "Scripts/Tables";
    private static string DIR_SEEDDATA = "Scripts/SeedData";
    private static string DIR_DROP = "Scripts/DropObjects";

    private static string TYPE_PROCEDURE = "PROCEDURE";
    private static string TYPE_VIEW = "VIEW";
    private static string TYPE_TABLE = "TABLE";

    private static string FILE_EXT = ".sql";

    private static Dictionary<String, String> _drop = new() {
        {DIR_STOREDPROCS, TYPE_PROCEDURE },
        {DIR_VIEWS, TYPE_VIEW }      
    };

    private static List<String> _create = new List<String> {
        DIR_TABLES, DIR_VIEWS, DIR_STOREDPROCS
    };


    public static void Build(string connectionString) {

        using (SqlConnection connection = new SqlConnection(connectionString)) {
            Server server = new Server(new ServerConnection(connection));

            //TODO:  create a script that lists scripts in order to run...

            foreach (var (dir, objType) in _drop) {
                DropObjects(server, dir, objType);
            }

            //tables are special, need a specific order
            RunScripts(server, DIR_DROP);

            foreach (var dir in _create) {
                RunScripts(server, dir);
            }

            RunScripts(server, DIR_SEEDDATA);

        }
    }


    private static void RunScripts(Server dbServer, String scriptDirectory) {
        List<String> scripts = Directory.EnumerateFiles(
               scriptDirectory, $"*{FILE_EXT}", SearchOption.AllDirectories)
               .ToList();

        scripts.ForEach(script => {
            dbServer.ConnectionContext.ExecuteNonQuery(File.ReadAllText(script));
        });
    }

    private static void DropObjects(Server dbServer, String objectDirectory, String objectType) {
        List<String> objects = Directory.EnumerateFiles(objectDirectory, "*.sql", SearchOption.AllDirectories)
            .Select(name => GetObjectNameFromFullPath(name))
            .ToList();

        objects.ForEach(item =>
        {
            string command = $"DROP {objectType} IF EXISTS dbo.[{item}]";
            dbServer.ConnectionContext.ExecuteNonQuery(command);
        });
    }

    private static string GetObjectNameFromFullPath(string fullPath) {
        String fileName = Path.GetFileName(fullPath);
        return fileName.Replace(FILE_EXT, "");
    }
}
