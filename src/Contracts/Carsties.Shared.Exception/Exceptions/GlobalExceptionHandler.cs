using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Carsties.Shared.ExceptionHandler.Exceptions;

public class ErrorResponse
{
    public string? Message { get; set; }
    public int StatusCode { get; set; }
}

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,

            _ => StatusCodes.Status500InternalServerError
        };
        var response = new ErrorResponse
        {
            Message = exception.Message,
            StatusCode = statusCode
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}
