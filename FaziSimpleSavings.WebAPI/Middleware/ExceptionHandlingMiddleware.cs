using FaziSimpleSavings.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;


public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode;
        List<string>? errors = null;
        string message = exception.Message;

        switch (exception)
        {
            case NotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                break;
            case ValidationAppException vex:
                statusCode = (int)HttpStatusCode.BadRequest;
                errors = vex.Errors;
                break;
            case ForbiddenException:
                statusCode = (int)HttpStatusCode.Forbidden;
                break;
            case ConflictException:
                statusCode = (int)HttpStatusCode.Conflict;
                break;
            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        var response = ApiResponse<string>.Fail(message, statusCode, errors);
        var payload = JsonSerializer.Serialize(response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(payload);
    }
}
