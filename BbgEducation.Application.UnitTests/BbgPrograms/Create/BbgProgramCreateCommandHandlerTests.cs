using AutoFixture;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.Create;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using FluentAssertions;
using NSubstitute;

namespace BbgEducation.Application.UnitTests.BbgPrograms.Create;
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
    public async void Handle_ShouldReturnNewProgram_WhenInputIsValid()
    {
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
    public async void Handle_Should_Fail_WhenNameExists()
    {
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
        result.AsT1.Should().NotBeNull();
        result.AsT1.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT1.GetType().Should().Be(typeof(NameExistsValidationFailed));
    }

}
