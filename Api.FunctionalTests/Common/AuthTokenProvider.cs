using BbgEducation.Api.Common.Authentication;
using BbgEducation.Application.Authentication.Common;
using System.Net;
using System.Net.Http.Json;

namespace Api.FunctionalTests.Common;
public static class AuthTokenProvider
{

    public static async Task SetTestAuthToken(HttpClient httpClient) {

        String authToken = await GetTestAuthToken(httpClient);

        if (!String.IsNullOrEmpty(authToken)) {
            httpClient.DefaultRequestHeaders.Authorization =
                 new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
        }
    }

    private static async Task<String> GetTestAuthToken(HttpClient httpClient) {
        var testUserEmail = "test@test.com";
        var testUserPassword = "123456";

        var response = await LoginUser(httpClient, testUserEmail, testUserPassword);
        if (response.StatusCode == HttpStatusCode.BadRequest) {
            var testUserFirstName = "Test";
            var testUserLastName = "User";
            response = await RegisterUser(httpClient, testUserFirstName, testUserLastName, testUserEmail, testUserPassword);
        }

        if (!response.IsSuccessStatusCode || response.Content == null) {
            return String.Empty;
        }

        var authResult = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
        return authResult!.Token;

    }

    private static async Task<HttpResponseMessage> LoginUser(HttpClient httpClient, 
        String email, String password) {        

        var loginRequest = new LoginRequest(email, password);
        return await httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
    }

    private static async Task<HttpResponseMessage> RegisterUser(HttpClient httpClient, 
        String firstName, String lastName, String email, String password) {

        var authRequest = new RegisterRequest(firstName, lastName, email, password);
        return await httpClient.PostAsJsonAsync("api/auth/register", authRequest);

    }
}
