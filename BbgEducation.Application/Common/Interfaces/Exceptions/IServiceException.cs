﻿using System.Net;

namespace BbgEducation.Application.Common.Interfaces.Exceptions;
public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}