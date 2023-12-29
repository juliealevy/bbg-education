using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BbgEducation.Application.Common;
public sealed record BbgError(string Code, string? Description = null) {

    public static BbgError None => new BbgError(string.Empty, string.Empty);
    
}

public static class AuthenticationErrors {

    public static readonly BbgError EmailExists = new BbgError("Register.EmailExists",
        "Email already exists");
}

public class Result
    
{
    protected internal Result(bool isSuccess, BbgError error) {

        if (isSuccess && error != BbgError.None ||
                !isSuccess && error == BbgError.None) {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, BbgError.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, BbgError.None);

    public static Result Failure(BbgError error) => new(false, error);

    public static Result<TValue> Failure<TValue>(BbgError error) => new (false, error);

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public BbgError Error { get; }

    public ProblemDetails Problem {
        get {
            return new ProblemDetails() {
                Status = (int)HttpStatusCode.UnprocessableEntity,
                Type = Error.Code,
                Detail = Error.Description,
                Title = "An error occurred"
            };
        }
    }

}

public class Result<TValue>: Result {

    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, BbgError error)
        : base(isSuccess, error) {
        _value = value;
    }

    protected internal Result(bool isSuccess, BbgError error)
        : base(isSuccess, error) {        
    }

    public TValue Value => IsSuccess ? _value! :
        throw new InvalidOperationException("The value of a failure result cannot be accessed");

}

