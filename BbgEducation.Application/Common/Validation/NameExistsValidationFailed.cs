using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Common.Validation;
public record NameExistsValidationFailed : ValidationFailed
{
    public NameExistsValidationFailed(string entityName) : base("Name", $"{entityName} Name already exists.") {
    }
}
