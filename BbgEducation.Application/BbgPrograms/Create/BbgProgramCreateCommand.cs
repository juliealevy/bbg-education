﻿using BbgEducation.Application.Common.Validation;
using MediatR;
using OneOf;

namespace BbgEducation.Application.BbgPrograms.Create;
public record BbgProgramCreateCommand(
    string Name,
    string Description) : IRequest<OneOf<int, ValidationFailed>>;
