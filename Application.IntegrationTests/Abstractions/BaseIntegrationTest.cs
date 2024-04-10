using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Database.Tests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Abstractions;
public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly IBbgProgramRepository ProgramRepository;
    protected readonly ICourseRepository CourseRepository;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory webAppFactory)
    {
        _scope = webAppFactory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        ProgramRepository = _scope.ServiceProvider.GetRequiredService<IBbgProgramRepository>();
        CourseRepository = _scope.ServiceProvider.GetRequiredService<ICourseRepository>();
        DbObjectBuilder.Build(webAppFactory.ConnectionString);
    }
}
