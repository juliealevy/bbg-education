using AutoFixture;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.BbgPrograms.Update;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OneOf.Types;

namespace BbgEducation.Application.UnitTests.BbgPrograms.Update;
public class BbgProgramUpdateCommandHandlerTests
{
    private readonly BbgProgramUpdateCommandHandler _testing;
    private readonly IBbgProgramRepository _programRepository = Substitute.For<IBbgProgramRepository>();
    private readonly IFixture _fixture = new Fixture();

    public BbgProgramUpdateCommandHandlerTests()
    {
        _testing = new BbgProgramUpdateCommandHandler(_programRepository);
    }

    [Fact]
    public async void Handle_ShouldReturnUpdatedProgram_WhenInputIsValid() {

        //arrange       
        var command = _fixture.Create<BbgProgramUpdateCommand>();
        var program = BbgProgram.Create(command.Id, command.Name, command.Description);

        _programRepository.GetProgramByIdAsync(command.Id, default).Returns(program);
        //this should not be called so returning true to be sure
        _programRepository.CheckProgramNameExistsAsync(command.Name,default).Returns(true);


        _programRepository.UpdateProgram(program);

        //act
        var result = await _testing.Handle(command, default);

        //assert
        result.Should().NotBeNull();

        result.IsT0.Should().BeTrue();
        Success T0Value = result.AsT0;        
        T0Value.Should().NotBeNull();
    }

    [Fact]
    public async void Handle_ShouldReturnNotFound_WhenIdIsInvalid() {

        //arrange
        var command = _fixture.Create<BbgProgramUpdateCommand>();        
        _programRepository.GetProgramByIdAsync(command.Id, default).ReturnsNull();

        //act
        var result = await _testing.Handle(command, default);

        //assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Should().Be(new NotFound());
    }   

    [Fact]
    public async void Handle_ShouldReturnFail_WhenChangedNameExists() {

        //arrange
        var command = _fixture.Create<BbgProgramUpdateCommand>();
        var program = BbgProgram.Create(command.Id, _fixture.Create<String>(), command.Description);

        _programRepository.GetProgramByIdAsync(command.Id,default).Returns(program);
        _programRepository.CheckProgramNameExistsAsync(command.Name,default).Returns(true);

        //act
        var result = await _testing.Handle(command, default);

        //assert
        result.Should().NotBeNull();
        result.IsT2.Should().BeTrue();
        result.AsT2.Should().NotBeNull();
        result.AsT2.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT2.GetType().Should().Be(typeof(NameExistsValidationFailed));

    }
}
