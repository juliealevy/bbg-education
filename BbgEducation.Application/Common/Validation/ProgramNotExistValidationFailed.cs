using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Common.Validation;
public record ProgramNotExistValidationFailed : ValidationFailed
{
    public ProgramNotExistValidationFailed(int programId) : base("ProgramId", $"Program with id {programId} does not exist.") {
    }
}
