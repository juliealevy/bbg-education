using System.Net;

namespace BbgEducation.Application.Common.Exceptions;
public class InvalidCredentialsException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public string ErrorMessage => "Email or Password is invalid.";
}
