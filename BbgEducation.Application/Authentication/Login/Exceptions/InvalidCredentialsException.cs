using System.Net;
using BbgEducation.Application.Common.Interfaces.Exceptions;

namespace BbgEducation.Application.Authentication.Login.Exceptions;
public class InvalidCredentialsException : Exception, IApplicationException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public string ErrorMessage => "Email or Password is invalid.";
}
