using Api.FunctionalTests.WebApp;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Database.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace Api.FunctionalTests.Common;
public class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    private IServiceScope _scope;
    protected HttpClient HttpClient;
    protected String EntityRootPath;
    protected ICourseRepository CourseRepository;

    protected BaseFunctionalTest(FunctionalTestWebAppFactory webAppfactory, String entityRootPath)
    {
        _scope = webAppfactory.Services.CreateScope();
        DbObjectBuilder.Build(webAppfactory.ConnectionString);
        HttpClient = webAppfactory.CreateClient();
        EntityRootPath = entityRootPath;
        CourseRepository = _scope.ServiceProvider.GetRequiredService<ICourseRepository>();
    }  

    protected async Task SetAuthToken() {
        await AuthTokenProvider.SetTestAuthToken(HttpClient);
    }

}
