using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace BbgEducation.Infrastructure.UnitTests.Persistance.Repositories.Common;
public class RepositoryTestDbContainerFactory : IAsyncLifetime
{

    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
       .Build();

    public string ConnectionString => _dbContainer.GetConnectionString();

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        _dbContainer.StopAsync();
        return _dbContainer.DisposeAsync().AsTask();
    }
}
