using BbgEducation.Database.Tests.Abstractions;

namespace BbgEducation.Database.Tests;
public class DatabaseTest : BaseDatabaseTest
{
    //basic tests to make sure database is created with expected objects

    public DatabaseTest(DatabaseTestWebAppFactory webFactory)
        : base(webFactory) {
    }

    [Fact]
    public void DB_CanQuery() {
        using (var connection = DbConnectionFactory.Create()) {
            connection.Open();
            using (var command = connection.CreateCommand()) {
                command.CommandText = "SELECT 1";
                var actual = command.ExecuteScalar() as int?;
                Assert.Equal(1, actual);

            }

        }
    }

    [Fact]
    public void DB_CanQueryUsers() {
        using (var connection = DbConnectionFactory.Create()) {
            connection.Open();
            using (var command = connection.CreateCommand()) {
                command.CommandText = "SELECT user_id, name, email, is_admin FROM [dbo].[User]";
                command.CommandType = System.Data.CommandType.Text;
                using (var reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        Assert.Equal("julie", reader[1].ToString().ToLower().Trim());
                    }
                }



            }

        }
    }

    [Fact]
    public void DB_CanQueryProgram() {
        using (var connection = DbConnectionFactory.Create()) {
            connection.Open();
            using (var command = connection.CreateCommand()) {
                command.CommandText = "SELECT 1 FROM [dbo].[Program]";
                var actual = command.ExecuteScalar() as int?;
                Assert.Equal(1, actual);


            }

        }
    }

    [Fact]
    public void DB_CanQuerySession() {
        using (var connection = DbConnectionFactory.Create()) {
            connection.Open();
            using (var command = connection.CreateCommand()) {
                command.CommandText = "SELECT 1 FROM [dbo].[Session]";
                var actual = command.ExecuteScalar() as int?;
                Assert.Equal(1, actual);
            }

        }
    }
}
