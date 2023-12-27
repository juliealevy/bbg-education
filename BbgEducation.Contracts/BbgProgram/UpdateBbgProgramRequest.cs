using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Contracts.BbgProgram;
public record UpdateBbgProgramRequest(
    string Id,
    string Name,
    string Description);

