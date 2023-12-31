using BbgEducation.Application.Common.Interfaces.Persistance;
using BbgEducation.Application.Common.Validation;
using BbgEducation.Domain.BbgProgramDomain;
using MediatR;
using OneOf;
using OneOf.Types;

namespace BbgEducation.Application.BbgPrograms.GetById;
public class BbgProgramGetByIdQueryHandler : IRequestHandler<BbgProgramGetByIdQuery, OneOf<BbgProgramResult, NotFound>>
{
    private readonly IBbgProgramRepository _repository;
    public BbgProgramGetByIdQueryHandler(IBbgProgramRepository repository)
    {
        _repository = repository;
    }

    public async Task<OneOf<BbgProgramResult, NotFound>> Handle(BbgProgramGetByIdQuery request, CancellationToken cancellationToken)
    {
        var program = await _repository.GetProgramByIdAsync(request.Id);

        return program == null ? new NotFound() : new BbgProgramResult((int)program.program_id!, program.program_name, 
            program.description);
    }
}
