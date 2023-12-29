using System.Net;
using BbgEducation.Application.Common.Interfaces.Exceptions;

namespace BbgEducation.Application.Authentication.Register.Exceptions;
public class DuplicateEmailException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "Email already exists";
}

