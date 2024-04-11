using Api.FunctionalTests.Common;
using Api.FunctionalTests.WebApp;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BbgEducation.Application.Courses.Common;

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
        string newId = await createResponse.Content.ReadAsStringAsync();

        //act
        HttpResponseMessage response = await HttpClient.GetAsync($"{EntityRootPath}/{newId}");

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact]
    public async Task GetById_ShouldReturnCourseResult_WhenIdExists() {

        //arrange
        await SetAuthToken();
        var request = CourseDataBuilders.BuildTestRequest();
        HttpResponseMessage createResponse = await HttpClient.PostAsJsonAsync(EntityRootPath, request);
        string newId = await createResponse.Content.ReadAsStringAsync();

        //act
        HttpResponseMessage response = await HttpClient.GetAsync($"{EntityRootPath}/{newId}");
        var courseResult = await response.Content.ReadFromJsonAsync<CourseResult>();

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        courseResult.Should().NotBeNull();
        courseResult!.Id.Should().Be(Convert.ToInt32(newId));
        courseResult.Name.Trim().Should().Be(request.Name);
        courseResult.IsPublic.Should().Be(request.isPublic);

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
}
