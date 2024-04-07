using Api.FunctionalTests.WebApp;
using BbgEducation.Database.Tests;

namespace Api.FunctionalTests.Common;
public class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{
    protected HttpClient HttpClient;

    protected BaseFunctionalTest(FunctionalTestWebAppFactory webAppfactory)
    {
        DbObjectBuilder.Build(webAppfactory.ConnectionString);
        HttpClient = webAppfactory.CreateClient();
    }  

    protected async Task SetAuthToken() {
        await AuthTokenProvider.SetTestAuthToken(HttpClient);
    }

}
