using BbgEducation.Application.Common.Interfaces.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Authentication.Register;

public class ClearCommandHandler : IRequestHandler<ClearCommand>
{
    private readonly IUserRepository _userRepository;

    public ClearCommandHandler(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task Handle(ClearCommand request, CancellationToken cancellationToken) {
        await Task.CompletedTask;
        _userRepository.ClearAllUsers();
    }
}
