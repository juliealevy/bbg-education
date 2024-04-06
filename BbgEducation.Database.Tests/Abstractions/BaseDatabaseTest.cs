using BbgEducation.Database.Tests;
using BbgEducation.Infrastructure.Persistance.Connections;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BbgEducation.Database.Tests.Abstractions;
public class BaseDatabaseTest : IClassFixture<DatabaseTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly ISQLConnectionFactory DbConnectionFactory;

    protected BaseDatabaseTest(DatabaseTestWebAppFactory webAppfactory)
    {
        _scope = webAppfactory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbConnectionFactory = new TestConnectionFactory(webAppfactory.ConnectionString);
        DbObjectBuilder.Build(webAppfactory.ConnectionString);
    }

}
