﻿using Api.FunctionalTests.Common;
using Api.FunctionalTests.WebApp;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using BbgEducation.Api.Common.Course;
using BbgEducation.Api.Common.BbgProgram;
using BbgEducation.Application.Common.Interfaces.Persistance;

namespace Api.FunctionalTests.Courses;
public class CreateCourseTests : BaseFunctionalTest
{   
   
    public CreateCourseTests(FunctionalTestWebAppFactory webAppfactory) 
        : base(webAppfactory, "api/courses") {
        
    }

    [Fact]
    public async Task CreateCourse_ShouldReturnCreated_WhenInputValid() {

        //arrange
        await SetAuthToken();
        var request = CourseDataBuilders.BuildTestRequest();

        //act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(EntityRootPath, request);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

    }

    [Fact]
    public async Task CreateCourse_ShouldReturnBadRequest_WhenNameEmpty() {

        //arrange
        await SetAuthToken();        
        var request = new CreateCourseRequest("", "blah", false);

        //act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(EntityRootPath, request);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);


    }

    [Fact]
    public async Task CreateCourse_ShouldReturnConflict_WhenNameExists() {

        //arrange
        await SetAuthToken();
        var request = CourseDataBuilders.BuildTestRequest();
        CourseRepository.AddCourse(request.Name, request.Description, request.IsPublic);

        //act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(EntityRootPath, request);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);   


    }

  

    [Fact]
    public async Task CreateCourse_ShouldReturnUnauthorized_WhenTokenMissing() {

        //arrange
       var request = CourseDataBuilders.BuildTestRequest();

        //act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(EntityRootPath, request);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);


    }

   
}
