using AutoFixture;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using FluentAssertions;
using NSubstitute;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.UnitTests.BbgPrograms;
public class BbgProgramCreateCommandHandlerTests
{
    private readonly BbgProgramCreateCommandHandler _testing;
    private readonly IBbgProgramRepository _programRepository = Substitute.For<IBbgProgramRepository>();
    private readonly IFixture _fixture = new Fixture();

    public BbgProgramCreateCommandHandlerTests()
    {
        _testing = new BbgProgramCreateCommandHandler(_programRepository);
    }

    [Fact]
    public async void Handle_ShouldReturnNewProgram_WhenInputIsValid() {
        //arrange
        var command = _fixture.Create<BbgProgramCreateCommand>();        
        _programRepository.CheckProgramNameExistsAsync(command.Name).Returns(false);
        
        var savedProgram = BbgProgram.Create(1, command.Name, command.Description);
        _programRepository.AddProgram(command.Name, command.Description).Returns(savedProgram);

        //act
        var result = await _testing.Handle(command, default);

        //assert
        result.Should().NotBeNull();

        result.IsT0.Should().BeTrue();
        BbgProgramResult? T0Value = result.AsT0;
        T0Value.Should().NotBeNull();        
        T0Value.Id.Should().Be(savedProgram.program_id);
        T0Value.Name.Should().Be(savedProgram.program_name);
        T0Value.Description.Should().Be(savedProgram.description);
    }


    [Fact]
    public async void Handle_ShouldReturnNewProgram_WhenNameExists() {
        //arrange
        var command = _fixture.Create<BbgProgramCreateCommand>();
        _programRepository.CheckProgramNameExistsAsync(command.Name).Returns(true);

        var savedProgram = BbgProgram.Create(1, command.Name, command.Description);
        _programRepository.AddProgram(command.Name, command.Description).Returns(savedProgram);

        //act
        var result = await _testing.Handle(command, default);

        //assert
        result.Should().NotBeNull();

        result.IsT1.Should().BeTrue();
        var validationFailResult = result.AsT1;
        validationFailResult.Should().NotBeNull();
        validationFailResult.Errors.Should().NotBeNull();
        validationFailResult.Errors.Count().Should().Be(1);
        validationFailResult.Errors.FirstOrDefault()!.ErrorMessage.Should().Be("Name already exists");
        
        
    }

}
