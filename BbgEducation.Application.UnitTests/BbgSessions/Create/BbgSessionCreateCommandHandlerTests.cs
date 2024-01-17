using AutoFixture;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.Create;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.BbgSessionDomain;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.UnitTests.BbgSessions.Create;
public class BbgSessionCreateCommandHandlerTests
{
    private readonly BbgSessionCreateCommandHandler _testing;
    private readonly IBbgSessionRepository _sessionRepository = Substitute.For<IBbgSessionRepository>();
    private readonly IBbgProgramRepository _programRepository = Substitute.For<IBbgProgramRepository>();
    private readonly IFixture _fixture = new Fixture();

    public BbgSessionCreateCommandHandlerTests() {
        _testing = new BbgSessionCreateCommandHandler(_sessionRepository, _programRepository);
    }

    [Fact]
    public async void Handle_ShouldReturnNewSession_WhenInputIsValid() {
        
        var program = GetNewProgram();
        var command = GetNewCommand(program);

        var newSession = BbgSession.Build(_fixture.Create<int>(), command.Name, command.Description,
            command.StartDate.ToDateTime(TimeOnly.Parse("12:00 AM")), command.EndDate.ToDateTime(TimeOnly.Parse("12:00 AM")),
            program);

        _sessionRepository.CheckSessionNameExistsAsync(command.Name,default).Returns(false);
        _programRepository.GetProgramByIdAsync(command.ProgramId, default).Returns(program);
        _sessionRepository.AddSession(command.ProgramId, command.Name, command.Description, command.StartDate, command.EndDate)
            .Returns((int)newSession.session_id!);

        var result = await _testing.Handle(command, default);

       
        result.Should().NotBeNull();

        result.IsT0.Should().BeTrue();
        int? T0Value = result.AsT0;
        T0Value.Should().NotBeNull();
        T0Value.Should().Be(newSession.session_id);
    }

    [Fact]
    public async void Handle_ShouldReturnFailed_WhenNameExists() {

        var program = GetNewProgram();
        var command = GetNewCommand(program);

        _sessionRepository.CheckSessionNameExistsAsync(command.Name, default).Returns(true);

        var result = await _testing.Handle(command, default);

        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT1.GetType().Should().Be(typeof(NameExistsValidationFailed));
    }

    [Fact]
    public async void Handle_ShouldReturnFailed_WhenProgramNotExists() {

        var program = GetNewProgram();
        var command = GetNewCommand(program);

        _sessionRepository.CheckSessionNameExistsAsync(command.Name, default).Returns(false);
        _programRepository.GetProgramByIdAsync((int)program.program_id!, default).ReturnsNull();

        var result = await _testing.Handle(command, default);

        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT1.GetType().Should().Be(typeof(ProgramNotExistValidationFailed));
    }


    private BbgSessionCreateCommand GetNewCommand(BbgProgram program) {

        return new BbgSessionCreateCommand((int)program.program_id!, _fixture.Create<string>(), _fixture.Create<string>(),
            DateOnly.Parse("09/20/2023"), DateOnly.Parse("12/20/2023"));
    }

    private BbgProgram GetNewProgram() {
        return BbgProgram.Create(_fixture.Create<int>(), _fixture.Create<string>(), _fixture.Create<string>());
    }
}
