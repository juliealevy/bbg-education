using BbgEducation.Database.Tests;
using BbgEducation.Infrastructure.Persistance.Connections;
using Testcontainers.MsSql;

namespace BbgEducation.Infrastructure.UnitTests.Persistance.Repositories.Common;
public abstract class BaseRepositoryTest : IClassFixture<RepositoryTestDbContainerFactory>
{
    protected ISQLConnectionFactory SQLConnectionFactory;

    protected BaseRepositoryTest(RepositoryTestDbContainerFactory factory)
    {
        SQLConnectionFactory = new TestConnectionFactory(factory.ConnectionString);
        DbObjectBuilder.Build(factory.ConnectionString);
    }

}
