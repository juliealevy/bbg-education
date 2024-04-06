using Application.IntegrationTests.Abstractions;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgPrograms.GetById;
using BbgEducation.Application.Common.Validation;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Programs;
public class GetByIdProgramTests : BaseIntegrationTest
{
    public GetByIdProgramTests(IntegrationTestWebAppFactory webAppFactory)
        : base(webAppFactory)
    {
    }

    [Fact]
    public async Task GetById_ShouldReturnProgram_WhenIdExists()
    {
        //arrange
        var createCommand = new BbgProgramCreateCommand("Program 2", "Description for Program 2");
        var createResult = await Sender.Send(createCommand);
        int newId = createResult.AsT0;

        var getQuery = new BbgProgramGetByIdQuery(newId);

        //act
        var getResult = await Sender.Send(getQuery);

        //assert
        getResult.Should().NotBeNull();
        getResult.IsT0.Should().BeTrue();
        BbgProgramResult program = getResult.AsT0;

        program.Should().NotBeNull();
        program.Name.Trim().Should().Be(createCommand.Name);


    }


    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenIdNotExists()
    {
        //arrange
        var getQuery = new BbgProgramGetByIdQuery(100);

        //act
        var getResult = await Sender.Send(getQuery);

        //assert
        getResult.Should().NotBeNull();
        getResult.IsT1.Should().BeTrue();
        getResult.AsT1.Should().NotBeNull();
        var failure = getResult.AsT1;
        failure.GetType().Should().Be(typeof(OneOf.Types.NotFound));


    }
}
