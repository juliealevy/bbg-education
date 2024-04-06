using BbgEducation.Api;
using BbgEducation.Infrastructure.Persistance.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using BbgEducation.Database.Tests;

namespace Application.IntegrationTests.Abstractions;
public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
       .Build();

    public string ConnectionString => _dbContainer.GetConnectionString();

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<ISQLConnectionFactory>();
            services.AddSingleton<ISQLConnectionFactory>(x =>
                            new TestConnectionFactory(_dbContainer.GetConnectionString())
            );

        });
    }

    public new Task DisposeAsync()
    {
        _dbContainer.StopAsync();
        return _dbContainer.DisposeAsync().AsTask();
    }
}
