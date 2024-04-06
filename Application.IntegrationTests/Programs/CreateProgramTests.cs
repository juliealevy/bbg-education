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
public class CreateProgramTests : BaseIntegrationTest
{
    public CreateProgramTests(IntegrationTestWebAppFactory webAppFactory)
        : base(webAppFactory)
    {
    }


    [Fact]
    public async Task Create_ShouldAddNewProgram_WhenValidCommand()
    {
        //arrange
        var command = new BbgProgramCreateCommand("Program 2", "Description for Program 2");

        //act
        var result = await Sender.Send(command);

        //assert
        result.Should().NotBeNull();
        result.IsT0.Should().BeTrue();
        int? programId = result.AsT0;
        programId.Should().NotBeNull();
        var newProgram = await ProgramRepository.GetProgramByIdAsync((int)programId, CancellationToken.None);
        newProgram.Should().NotBeNull();

    }

    [Fact]
    public async Task Create_ShouldFail_WhenNameEmpty()
    {
        //arrange
        var command = new BbgProgramCreateCommand("", "Description for Program 2");

        //act
        var result = await Sender.Send(command);

        //assert
        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Errors.Should().NotBeNull().And.HaveCount(2);
        result.AsT1.GetType().Should().Be(typeof(ValidationFailed));

    }

    [Fact]
    public async Task Create_ShouldFail_WhenNameExists()
    {
        //arrange
        var command = new BbgProgramCreateCommand("program1", "Description for Program 2");

        //act
        var result = await Sender.Send(command);

        //assert
        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT1.GetType().Should().Be(typeof(NameExistsValidationFailed));

   }
   
}
