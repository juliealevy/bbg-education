using BbgEducation.Application.Common.Errors;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using BbgEducation.Infrastructure.Persistance.Repositories;
using BbgEducation.Infrastructure.UnitTests.Persistance.Repositories.Common;
using FluentAssertions;

namespace BbgEducation.Infrastructure.UnitTests.Persistance.Repositories;
public class ProgramTests: BaseRepositoryTest
{
    private readonly IBbgProgramRepository _underTest;

    public ProgramTests(RepositoryTestDbContainerFactory containerFactory)
        : base(containerFactory) {
        _underTest = new BbgProgramRepository(SQLConnectionFactory);
    }

    [Fact]
    public async Task AddProgram_ReturnsId_WhenInputValid() {
        string testName = "Test Program 2";
        string testDescription = "Test Program 2 Description";

        var newId = _underTest.AddProgram(testName, testDescription);

        newId.Should().BePositive();
        BbgProgram fetchedProgram =  await _underTest.GetProgramByIdAsync(newId, CancellationToken.None);
        fetchedProgram.Should().NotBeNull();
        fetchedProgram.program_id.Should().Be(newId);
        fetchedProgram.program_name.Trim().Should().Be(testName);
        
    }

    [Fact]
    public void AddProgram_ThrowsException_WhenNameExists() {
        string testName = "program1";
        string testDescription = "Test Program 2 Description";

        Action action = () => _underTest.AddProgram(testName, testDescription);

        action.Should().Throw<DBException>()
            .WithMessage("*UNIQUE KEY*");

    }

    [Fact]
    public async Task GetById_ReturnsProgram_WhenIdExists() {
        int existingId = 1;  //seeded in

        BbgProgram fetchedProgram = await _underTest.GetProgramByIdAsync(existingId, CancellationToken.None);
        fetchedProgram.Should().NotBeNull();
        fetchedProgram.program_id.Should().Be(existingId);

    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenIdNotExists() {
        int invalidId = 100;

        BbgProgram fetchedProgram = await _underTest.GetProgramByIdAsync(invalidId, CancellationToken.None);
        fetchedProgram.Should().BeNull();

    }

}
