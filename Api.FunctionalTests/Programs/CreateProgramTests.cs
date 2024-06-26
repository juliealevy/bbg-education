﻿using Api.FunctionalTests.Common;
using Api.FunctionalTests.WebApp;
using BbgEducation.Api.Common.BbgProgram;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Api.FunctionalTests.Programs;
public class CreateProgramTests : BaseFunctionalTest
{
    public CreateProgramTests(FunctionalTestWebAppFactory webAppfactory) 
        : base(webAppfactory, "api/programs") {

    }

    [Fact]
    public async Task CreateProgram_ShouldReturnCreated_WhenInputValid() {

        //arrange
        await SetAuthToken();
        var request = new CreateBbgProgramRequest("Program 2", "Description for Program 2");

        //act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(EntityRootPath, request);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);


    }

    [Fact]
    public async Task CreateProgram_ShouldReturnBadRequest_WhenNameEmpty() {

        //arrange
        await SetAuthToken();
        var request = new CreateBbgProgramRequest("", "blah");

        //act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(EntityRootPath, request);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        
    }

    [Fact]
    public async Task CreateProgram_ShouldReturnConflict_WhenNameExists() {

        //arrange
        await SetAuthToken();
        var request = new CreateBbgProgramRequest("program1", "blah");

        //act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(EntityRootPath, request);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);


    }

    [Fact]
    public async Task CreateProgram_ShouldReturnUnauthorized_WhenTokenMissing() {

        //arrange
        var request = new CreateBbgProgramRequest("program 2", "blah");

        //act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(EntityRootPath, request);

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);


    }
}
