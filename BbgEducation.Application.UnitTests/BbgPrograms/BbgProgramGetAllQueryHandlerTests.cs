using AutoFixture;
using BbgEducation.Application.BbgPrograms.GetAll;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using FluentAssertions;
using NSubstitute;

namespace BbgEducation.Application.UnitTests.BbgPrograms;
public class BbgProgramGetAllQueryHandlerTests
{
    private readonly BbgProgramGetAllQueryHandler _testing;
    private readonly IBbgProgramRepository _programRepository =  Substitute.For<IBbgProgramRepository>();
    private readonly IFixture _fixture = new Fixture();

    public BbgProgramGetAllQueryHandlerTests() {
        _testing = new BbgProgramGetAllQueryHandler(_programRepository);
    }

    [Fact]
    public async void Handle_ShouldReturnList_WhenValid() {

        //arrange

        IEnumerable<BbgProgram> programs = new List<BbgProgram> {
            BbgProgram.Create(1, "One", "One Description"),
            BbgProgram.Create(2, "Two", "Two Description"),
            BbgProgram.Create(3, "Three", "Three Description")
        };

        _programRepository.GetProgramsAsync().Returns<IEnumerable<BbgProgram>>(programs);

        var query = _fixture.Create<BbgProgramGetAllQuery>();

        //act
        var result = await _testing.Handle(query, default);

        //assert
        result.Should().NotBeNull();
        result.Count.Should().Be(3);
        result.FirstOrDefault().Should().NotBeNull();

        var firstResult = result.OrderBy(x => x.Id).FirstOrDefault();
        var firstSeed = programs.OrderBy(x => x.program_id).FirstOrDefault();

        firstResult?.Id.Should().Be(firstSeed?.program_id);
        firstResult?.Name.Should().Be(firstSeed?.program_name);
        firstResult?.Description.Should().Be(firstSeed?.description);


    }   

}
