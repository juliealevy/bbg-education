using BbgEducation.Application.Common.Interfaces.Exceptions;
using System.Net;

namespace BbgEducation.Application.BbgPrograms.Exceptions;
public class DuplicateProgramNameException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;

    public string ErrorMessage => "Program name already exists";
}
