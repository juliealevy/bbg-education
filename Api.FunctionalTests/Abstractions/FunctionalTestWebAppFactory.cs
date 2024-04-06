using BbgEducation.Api;
using BbgEducation.Database.Tests;
using BbgEducation.Infrastructure.Persistance.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace Api.FunctionalTests.Abstractions;
public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()        
        .Build();

    public String ConnectionString => _dbContainer.GetConnectionString();

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<ISQLConnectionFactory>();
            services.AddSingleton<ISQLConnectionFactory>( x => 
                            new TestConnectionFactory(_dbContainer.GetConnectionString())
            );

        });
    }


    public Task InitializeAsync() {
       return _dbContainer.StartAsync();       
    }

    public new Task DisposeAsync() {
        _dbContainer.StopAsync();
        return _dbContainer.DisposeAsync().AsTask();
    }
}
