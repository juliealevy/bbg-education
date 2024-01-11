using AutoFixture;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.GetByProgramId;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Application.UnitTests.BbgSessions.Common;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.BbgSessionDomain;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BbgEducation.Application.UnitTests.BbgSessions.GetByProgramId;
public class BbgSessionGetByProgramIdQueryHandlerTests
{
    private readonly BbgSessionGetByProgramIdQueryHandler _testing;
    private readonly IBbgSessionRepository _sessionRepository = Substitute.For<IBbgSessionRepository>();
    private readonly IBbgProgramRepository _programRepository = Substitute.For<IBbgProgramRepository>();    
    private readonly IFixture _fixture = new Fixture();

    public BbgSessionGetByProgramIdQueryHandlerTests()
    {
        _testing = new BbgSessionGetByProgramIdQueryHandler(_programRepository, _sessionRepository);
    }
   

    [Fact]
    public async void Handle_ShouldReturnSessionList_WhenInputIsValid() {

        //arrange
        BbgSessionGetByProgramIdQuery query = _fixture.Create<BbgSessionGetByProgramIdQuery>();

        BbgProgram program = BbgProgram.Create(query.ProgramId,
            _fixture.Create<string>(), _fixture.Create<string>());
        _programRepository.GetProgramByIdAsync(query.ProgramId).Returns(program);


        IEnumerable<BbgSession> sessions = new List<BbgSession> {
            BbgSessionData.GenerateForProgram(program, _fixture),
            BbgSessionData.GenerateForProgram(program, _fixture),
            BbgSessionData.GenerateForProgram(program, _fixture)
        };

        _sessionRepository.GetSessionsByProgramId(query.ProgramId).Returns(sessions);

        //act
        var results = await _testing.Handle(query, default);

        //assert
        results.IsT0.Should().BeTrue();

        List<BbgSessionResult>? resultValue = results.AsT0;
        resultValue.Should().NotBeNull();
        resultValue.Count().Should().Be(sessions.Count());

        resultValue.Any(r => !r.Program.Id.Equals(query.ProgramId)).Should().BeFalse();

        var firstSession = sessions.OrderBy(s => s.session_id).First();
        var firstResult = resultValue.OrderBy(r => r.Id).First();

        firstSession.Should().NotBeNull();
        firstResult.Should().NotBeNull();
        firstSession.session_id.Should().Be(firstResult.Id);
        firstSession.session_name.Should().Be(firstResult.Name);
        firstSession.description.Should().Be(firstResult.Description);
        firstSession.session_program.program_id.Should().Be(firstResult.Program.Id);

    }

    [Fact]
    public async void Handle_ShouldReturnEmptyList_WhenNoData() {

        //arrange
        BbgSessionGetByProgramIdQuery query = _fixture.Create<BbgSessionGetByProgramIdQuery>();

        BbgProgram program = BbgProgram.Create(query.ProgramId,
            _fixture.Create<string>(), _fixture.Create<string>());
        _programRepository.GetProgramByIdAsync(query.ProgramId).Returns(program);

        IEnumerable<BbgSession> sessions = new List<BbgSession>();

        _sessionRepository.GetSessionsByProgramId(query.ProgramId).Returns(sessions);

        //act
        var result = await _testing.Handle(query, default);

        List<BbgSessionResult>? resultValue = result.AsT0;
        resultValue.Should().NotBeNull();
        resultValue.Count().Should().Be(0);

    }

    [Fact]
    public async void Handle_ShouldFail_WhenProgramNotExists() {

        //arrange
        BbgSessionGetByProgramIdQuery query = _fixture.Create<BbgSessionGetByProgramIdQuery>();

        BbgProgram program = BbgProgram.Create(query.ProgramId,
            _fixture.Create<string>(), _fixture.Create<string>());
        _programRepository.GetProgramByIdAsync(query.ProgramId).ReturnsNull<BbgProgram>();


        //act
        var result = await _testing.Handle(query, default);

        //assert       

        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Errors.Should().NotBeNull().And.HaveCount(1);
        result.AsT1.GetType().Should().Be(typeof(ProgramNotExistValidationFailed));
    }
   
}
