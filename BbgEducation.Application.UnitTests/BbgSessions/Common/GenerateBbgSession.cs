using AutoFixture;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Domain.BbgSessionDomain;

namespace BbgEducation.Application.UnitTests.BbgSessions.Common;
public static class BbgSessionData
{
    public static BbgSession GenerateForProgram(BbgProgram program, IFixture fixture) {

        var startDate = fixture.Create<DateTime>();
        var endDate = startDate.AddDays(100);

        return BbgSession.Build(
            fixture.Create<int>(),
            fixture.Create<String>(),
            fixture.Create<String>(),
            startDate,
            endDate,
            program);
    }

    public static BbgSession GenerateForProgramAndSession(int sessionId, BbgProgram program, IFixture fixture) {

        var startDate = fixture.Create<DateTime>();
        var endDate = startDate.AddDays(100);

        return BbgSession.Build(
            sessionId,
            fixture.Create<String>(),
            fixture.Create<String>(),
            startDate,
            endDate,
            program);
    }
  
    public static BbgSession Generate(IFixture fixture) {

        var startDate = fixture.Create<DateTime>();
        var endDate = startDate.AddDays(100);
        var program = BbgProgram.Create(
            fixture.Create<int>(), fixture.Create<String>(), fixture.Create<String>());

        return BbgSession.Build(
            fixture.Create<int>(),
            fixture.Create<String>(),
            fixture.Create<String>(),
            startDate,
            endDate,
            program);
    }
}
