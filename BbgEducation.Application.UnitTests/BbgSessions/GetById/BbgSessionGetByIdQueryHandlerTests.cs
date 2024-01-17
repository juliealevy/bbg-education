using AutoFixture;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.GetById;
using BbgEducation.Application.BbgSessions.GetByProgramId;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Application.UnitTests.BbgSessions.Common;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.BbgSessionDomain;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.UnitTests.BbgSessions.GetById;
public class BbgSessionGetByIdQueryHandlerTests
{
    private readonly BbgSessionGetByIdQueryHandler _testing;
    private readonly IBbgProgramRepository _programRepository = Substitute.For<IBbgProgramRepository>();
    private readonly IBbgSessionRepository _sessionRepository = Substitute.For<IBbgSessionRepository>();
    private readonly IFixture _fixture = new Fixture();

    public BbgSessionGetByIdQueryHandlerTests()
    {
        _testing = new BbgSessionGetByIdQueryHandler(_programRepository, _sessionRepository);
    }

    [Fact]
    public async void Handle_ShouldReturnSession_WhenInputIsValid() {
        //arrange
        var query = _fixture.Create<BbgSessionGetByIdQuery>();
        var program = BbgProgram.Create(query.ProgramId,
            _fixture.Create<string>(), _fixture.Create<string>());
        var session = BbgSessionData.GenerateForProgramAndSession(query.SessionId, program, _fixture);        

        _programRepository.GetProgramByIdAsync(query.ProgramId, default).Returns(program);
        _sessionRepository.GetSessionByIdAsync(query.SessionId, default).Returns(session);

        //act
        var result = await _testing.Handle(query, default);

        //assert
        result.IsT0.Should().BeTrue();
        BbgSessionResult? resultValue = result.AsT0;
        resultValue.Should().NotBeNull();
        resultValue.Id.Should().Be(query.SessionId);
        resultValue.Program.Id.Should().Be(query.ProgramId);

    }

    [Fact]
    public async void Handle_ShouldReturnFailed_WhenProgramNotExists() {
        //arrange
        var query = _fixture.Create<BbgSessionGetByIdQuery>();
        var program = BbgProgram.Create(query.ProgramId,
            _fixture.Create<string>(), _fixture.Create<string>());        

        _programRepository.GetProgramByIdAsync(query.ProgramId, default).ReturnsNull<BbgProgram>();

        
        //act
        var result = await _testing.Handle(query, default);

        //assert
        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT1.GetType().Should().Be(typeof(ProgramNotExistValidationFailed));

    }

    [Fact]
    public async void Handle_ShouldReturnNotFound_WhenSessionNotExists() {
        //arrange
        var query = _fixture.Create<BbgSessionGetByIdQuery>();
        var program = BbgProgram.Create(query.ProgramId,
            _fixture.Create<string>(), _fixture.Create<string>());

        _programRepository.GetProgramByIdAsync(query.ProgramId, default).Returns<BbgProgram>(program);
        _sessionRepository.GetSessionByIdAsync(query.SessionId, default).ReturnsNull<BbgSession>();


        //act
        var result = await _testing.Handle(query, default);

        //assert
        result.Should().NotBeNull();
        result.IsT2.Should().BeTrue();
        result.AsT2.Should().NotBeNull();
        result.AsT2.Should().Be(new NotFound());

    }
}
