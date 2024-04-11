using Api.FunctionalTests.Common;
using Api.FunctionalTests.WebApp;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using BbgEducation.Application.Courses.Common;
using BbgEducation.Api.Common.Hal.Resources;
using BbgEducation.Api.Common.Hal.Links;

namespace Api.FunctionalTests.Courses;
public class GetCourseByIdTests : BaseFunctionalTest
{
    public GetCourseByIdTests(FunctionalTestWebAppFactory webAppfactory) 
        : base(webAppfactory, "api/courses") {
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenIdExists() {

        //arrange
        await SetAuthToken();
        var request = CourseDataBuilders.BuildTestRequest();
        HttpResponseMessage createResponse = await HttpClient.PostAsJsonAsync(EntityRootPath, request);
        var href = await GetByIdHrefFromRepresentation(createResponse);

        //act
        HttpResponseMessage response = await HttpClient.GetAsync(href);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact]
    public async Task GetById_ShouldReturnCourseResult_WhenIdExists() {

        //arrange
        await SetAuthToken();
        var request = CourseDataBuilders.BuildTestRequest();
        HttpResponseMessage createResponse = await HttpClient.PostAsJsonAsync(EntityRootPath, request);
        var href = await GetByIdHrefFromRepresentation(createResponse);

        //act
        HttpResponseMessage response = await HttpClient.GetAsync(href);
        var courseResult = await response.Content.ReadFromJsonAsync<CourseResult>();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        courseResult.Should().NotBeNull();        
        courseResult!.Name.Trim().Should().Be(request.Name);
        courseResult.IsPublic.Should().Be(request.IsPublic);

    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenIdNotExists() {

        //arrange
        await SetAuthToken();
        var invalidId = 100;

        //act
        HttpResponseMessage response = await HttpClient.GetAsync($"{EntityRootPath}/{invalidId}");

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    private async Task<TestRepresentation> createCourse() {
        var request = CourseDataBuilders.BuildTestRequest();
        HttpResponseMessage createResponse = await HttpClient.PostAsJsonAsync(EntityRootPath, request);
        return await createResponse.Content.ReadFromJsonAsync<TestRepresentation>();
    }

    private async Task<string> GetByIdHrefFromRepresentation(HttpResponseMessage createCourseResponse) {
        
        var representation = await createCourseResponse.Content.ReadFromJsonAsync<TestRepresentation>();
        var getByIdLink = representation!._links[LinkRelations.Course.GET_BY_ID];
        var href = getByIdLink.First().Href;
        return href;
    }
}
