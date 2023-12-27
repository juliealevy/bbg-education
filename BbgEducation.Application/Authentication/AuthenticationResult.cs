using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Authentication;
public record AuthenticationResult(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);

