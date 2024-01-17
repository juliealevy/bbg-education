using AutoFixture;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.Update;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Application.UnitTests.BbgSessions.Common;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.BbgSessionDomain;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OneOf.Types;

namespace BbgEducation.Application.UnitTests.BbgSessions.Update;
public class BbgSessionUpdateCommandHandlerTests
{
    private readonly BbgSessionUpdateCommandHandler _testing;
    private readonly IBbgSessionRepository _repository = Substitute.For<IBbgSessionRepository>();
    private readonly IBbgProgramRepository _programRepository = Substitute.For<IBbgProgramRepository>();
    private readonly IFixture _fixture = new Fixture();

    public BbgSessionUpdateCommandHandlerTests() {
        _testing = new BbgSessionUpdateCommandHandler(_repository, _programRepository);
    }

    [Fact]
    public async void Handle_ShouldReturnUpdatedSession_WhenInputIsValid() {
        var command = new BbgSessionUpdateCommand(_fixture.Create<int>(), _fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>(),
            DateOnly.FromDateTime(_fixture.Create<DateTime>()), DateOnly.FromDateTime(_fixture.Create<DateTime>()));

        //program before upate using same session id as command only
        var existingProgram = BbgProgram.Create(_fixture.Create<int>(), "", "");
        var existingSession = BbgSessionData.GenerateForProgramAndSession(command.SessionId,
            existingProgram, _fixture);

        //updated session based on command
        var newProgram = BbgProgram.Create(command.ProgramId, "", "");
        var updatedSession = BbgSession.Build(command.SessionId, command.Name, command.Description,
            command.StartDate.ToDateTime(TimeOnly.Parse("12:00 AM")), command.EndDate.ToDateTime(TimeOnly.Parse("12:00 AM")),
            newProgram);

        _repository.GetSessionByIdAsync(command.SessionId, default).Returns(existingSession);
        _programRepository.GetProgramByIdAsync(command.ProgramId, default).Returns(newProgram);
        _repository.CheckSessionNameExistsAsync(command.Name, default).Returns(false);
        _repository.UpdateSession(updatedSession);

        var result = await _testing.Handle(command, default);

        result.Should().NotBeNull();

        result.IsT0.Should().BeTrue();
        Success? resultValue = result.AsT0;
        resultValue.Should().NotBeNull();

    }

    [Fact]
    public async void Handle_ShouldReturnNotFound_WhenSessionNotExists() {
        var command = new BbgSessionUpdateCommand(_fixture.Create<int>(), _fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>(),
            DateOnly.FromDateTime(_fixture.Create<DateTime>()), DateOnly.FromDateTime(_fixture.Create<DateTime>()));

        _repository.GetSessionByIdAsync(command.SessionId, default).ReturnsNull<BbgSession>();

        var result = await _testing.Handle(command, default);

        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Should().Be(new NotFound());
    }

    [Fact]
    public async void Handle_ShouldFail_WhenNewProgramNotExists() {
        var command = new BbgSessionUpdateCommand(_fixture.Create<int>(), _fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>(),
            DateOnly.FromDateTime(_fixture.Create<DateTime>()), DateOnly.FromDateTime(_fixture.Create<DateTime>()));

        //program before upate using same session id as command only
        var existingProgram = BbgProgram.Create(_fixture.Create<int>(), "", "");
        var existingSession = BbgSessionData.GenerateForProgramAndSession(command.SessionId,
            existingProgram, _fixture);

        _repository.GetSessionByIdAsync(command.SessionId, default).Returns(existingSession);
        _programRepository.GetProgramByIdAsync(command.ProgramId, default).ReturnsNull<BbgProgram>();

        var result = await _testing.Handle(command, default);

        result.Should().NotBeNull();
        result.IsT2.Should().BeTrue();
        result.AsT2.Should().NotBeNull();
        result.AsT2.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT2.GetType().Should().Be(typeof(ProgramNotExistValidationFailed));
    }

    [Fact]
    public async void Handle_ShouldFail_WhenSessionNameExists() {
        var command = new BbgSessionUpdateCommand(_fixture.Create<int>(), _fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>(),
           DateOnly.FromDateTime(_fixture.Create<DateTime>()), DateOnly.FromDateTime(_fixture.Create<DateTime>()));

        //program before upate using same session id and program name
        var existingProgram = BbgProgram.Create(command.ProgramId, "", "");
        var existingSession = BbgSessionData.GenerateForProgramAndSession(command.SessionId,
            existingProgram, _fixture);

        //updated session based on command
        var newProgram = BbgProgram.Create(command.ProgramId, "", "");
        var updatedSession = BbgSession.Build(command.SessionId, command.Name, command.Description,
            command.StartDate.ToDateTime(TimeOnly.Parse("12:00 AM")), command.EndDate.ToDateTime(TimeOnly.Parse("12:00 AM")),
            newProgram);

        _repository.GetSessionByIdAsync(command.SessionId, default).Returns(existingSession);
        _programRepository.GetProgramByIdAsync(command.ProgramId, default).Returns(newProgram);
        _repository.CheckSessionNameExistsAsync(command.Name, default).Returns(true);

        var result = await _testing.Handle(command, default);

        result.Should().NotBeNull();
        result.IsT2.Should().BeTrue();
        result.AsT2.Should().NotBeNull();
        result.AsT2.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT2.GetType().Should().Be(typeof(NameExistsValidationFailed));

    }
}
