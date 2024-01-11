using AutoFixture;
using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgSessions.Common;
using BbgEducation.Application.BbgSessions.GetAll;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.UnitTests.BbgSessions.Common;
using BbgEducation.Domain.BbgSessionDomain;
using FluentAssertions;
using NSubstitute;

namespace BbgEducation.Application.UnitTests.BbgSessions.GetAll;
public class BbgSessionGetAllQueryHandlerTests
{
    private readonly BbgSessionGetAllQueryHandler _testing;
    private readonly IBbgSessionRepository _sessionRepository = Substitute.For<IBbgSessionRepository>();
    private readonly IFixture _fixture = new Fixture();

    public BbgSessionGetAllQueryHandlerTests()
    {
        _testing = new BbgSessionGetAllQueryHandler(_sessionRepository);
    }

    [Fact]
    public async void Handle_ReturnsSessions_WhenValid() {

        BbgSessionGetAllQuery query = _fixture.Create< BbgSessionGetAllQuery>();

        IEnumerable<BbgSession> sessions = new List<BbgSession> {
            BbgSessionData.Generate(_fixture),
            BbgSessionData.Generate(_fixture),
            BbgSessionData.Generate(_fixture)
        };

        _sessionRepository.GetAllSessions().Returns(sessions);

        var result = await _testing.Handle(query, default);

        result.Should().NotBeNull();
        result.IsT0.Should().BeTrue();
        List<BbgSessionResult>? resultValue = result.AsT0;
        resultValue.Should().NotBeNull();
        resultValue.Count.Should().Be(3);
        resultValue.FirstOrDefault().Should().NotBeNull();

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

        BbgSessionGetAllQuery query = _fixture.Create<BbgSessionGetAllQuery>();

        IEnumerable<BbgSession> sessions = new List<BbgSession>();

        _sessionRepository.GetAllSessions().Returns(sessions);

        var result = await _testing.Handle(query, default);

        result.Should().NotBeNull();
        result.IsT0.Should().BeTrue();
        List<BbgSessionResult>? resultValue = result.AsT0;
        resultValue.Should().NotBeNull();
        resultValue.Count.Should().Be(0);

    }
}
