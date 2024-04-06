using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using BbgEducation.Api;
using BbgEducation.Infrastructure.Persistance.Connections;


namespace BbgEducation.Database.Tests.Abstractions;
public class DatabaseTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
       .Build();

    public string ConnectionString => _dbContainer.GetConnectionString();
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

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        _dbContainer.StopAsync();
        return _dbContainer.DisposeAsync().AsTask();
    }
}
