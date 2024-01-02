using BbgEducation.Application.BbgPrograms.Common;
using BbgEducation.Application.BbgPrograms.GetById;
using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Domain.BbgProgramDomain;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.UnitTests.BbgPrograms;
public class BbgProgramGetByIdQueryHandlerTests
{
    private readonly BbgProgramGetByIdQueryHandler _testing;
    private readonly IBbgProgramRepository _programRepository = Substitute.For<IBbgProgramRepository>();

    public BbgProgramGetByIdQueryHandlerTests()
    {
        _testing = new BbgProgramGetByIdQueryHandler(_programRepository);
    }

    [Fact]
    public async void Handle_ShouldReturnProgram_WhenIdIsValid() {
        //arrange
        var program = BbgProgram.Create(1, "One", "One Description");
        _programRepository.GetProgramByIdAsync((int)program.program_id!).Returns(program);

        //act
        var query = new BbgProgramGetByIdQuery((int)program.program_id);

        var result = await _testing.Handle(query, default);


        //assert
        result.IsT0.Should().BeTrue();

        BbgProgramResult? T0Value = result.AsT0;
        T0Value.Should().NotBeNull(); 
        T0Value.Id.Should().Be(program.program_id!);
        T0Value.Name.Should().Be(program.program_name);
        T0Value.Description.Should().Be(program.description);
    }

    [Fact]
    public async void Handle_ShouldReturnNotFound_WhenIdIsNotValid() {
        //arrange
        var program = BbgProgram.Create(-1, "One", "One Description");
        _programRepository.GetProgramByIdAsync((int)program.program_id!).ReturnsNull();

        //act
        var query = new BbgProgramGetByIdQuery((int)program.program_id);

        var result = await _testing.Handle(query, default);


        //assert
        result.IsT1.Should().BeTrue();
        result.AsT1.Should().NotBeNull();
        result.AsT1.Should().Be(new NotFound());
    }   
}
