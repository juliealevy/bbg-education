using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.GetById;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OneOf.Types;

namespace BbgEducation.Application.UnitTests.BbgPrograms.GetById;
public class BbgProgramGetByIdQueryHandlerTests
{
    private readonly BbgProgramGetByIdQueryHandler _testing;
    private readonly IBbgProgramRepository _programRepository = Substitute.For<IBbgProgramRepository>();

    public BbgProgramGetByIdQueryHandlerTests()
    {
        _testing = new BbgProgramGetByIdQueryHandler(_programRepository);
    }

    [Fact]
    public async void Handle_ShouldReturnProgram_WhenIdIsValid()
    {
        //arrange
        var program = BbgProgram.Create(1, "One", "One Description");
        _programRepository.GetProgramByIdAsync((int)program.program_id!, default).Returns(program);

        //act
        var query = new BbgProgramGetByIdQuery((int)program.program_id);

        var result = await _testing.Handle(query, default);


        //assert
        result.Should().NotBeNull();

        result.IsT0.Should().BeTrue();
        BbgProgramResult? returnValue = result.AsT0;
        returnValue.Should().NotBeNull();
        returnValue.Id.Should().Be(program.program_id!);
        returnValue.Name.Should().Be(program.program_name);
        returnValue.Description.Should().Be(program.description);
    }

    [Fact]
    public async void Handle_ShouldReturnNotFound_WhenIdIsNotValid()
    {
        //arrange
        var program = BbgProgram.Create(-1, "One", "One Description");
        _programRepository.GetProgramByIdAsync((int)program.program_id!,default).ReturnsNull();

        //act
        var query = new BbgProgramGetByIdQuery((int)program.program_id);

        var result = await _testing.Handle(query, default);


        //assert
        result.Should().NotBeNull();
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Should().Be(new NotFound());
    }
}
