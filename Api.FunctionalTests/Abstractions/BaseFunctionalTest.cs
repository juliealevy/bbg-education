using BbgEducation.Api.Common.Authentication;
using BbgEducation.Application.Authentication.Common;
using BbgEducation.Database.Tests;
using System.Net.Http.Json;

namespace Api.FunctionalTests.Abstractions;
public class BaseFunctionalTest: IClassFixture<FunctionalTestWebAppFactory>
{
    protected HttpClient HttpClient;

    protected BaseFunctionalTest(FunctionalTestWebAppFactory webAppfactory)
    {
        DbObjectBuilder.Build(webAppfactory.ConnectionString);
        HttpClient = webAppfactory.CreateClient(); 
    }

    protected async Task SetAuthToken() {
        HttpResponseMessage authResponse;

        var authRequest = new RegisterRequest("Julie", "Levy", "test@test.com", "123456");
        authResponse = await HttpClient.PostAsJsonAsync<RegisterRequest>("api/auth/register", authRequest);

        if (authResponse.StatusCode == System.Net.HttpStatusCode.Conflict) {
            var loginRequest = new LoginRequest("test@test.com", "123456");
            authResponse = await HttpClient.PostAsJsonAsync<LoginRequest>("api/auth/login", loginRequest);
        }

        if (authResponse.IsSuccessStatusCode) {
            AuthenticationResult responseContent = await authResponse
                .Content
                .ReadFromJsonAsync<AuthenticationResult>();

            HttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseContent.Token);
        }
    }
    
}
