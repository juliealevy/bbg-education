using BbgEducation.Api;
using BbgEducation.Database.Tests;
using BbgEducation.Infrastructure.Persistance.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace Api.FunctionalTests.WebApp;
public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .Build();

    public string ConnectionString => _dbContainer.GetConnectionString();

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<ISQLConnectionFactory>();
            services.AddSingleton<ISQLConnectionFactory>(x =>
                            new TestConnectionFactory(_dbContainer.GetConnectionString())
            );

            /*             * 
             * fakes authentication for all calls.  an option, but i'm going to
             * do actual authentication since these functional tests are intended to
             * recreate the api caller experience
             */
            //services.AddAuthentication("TestScheme")
            //.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });

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
