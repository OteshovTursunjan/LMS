using LMS.Application.Model;
using LMS.Domain.Exceptions;
using Newtonsoft.Json;

namespace LMS.API.Middleware;

public class ExceptionHandlerMiddleware
{

    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private Task HandleException(HttpContext context, Exception ex)
    {
        _logger.LogError(ex.Message);

        var code = StatusCodes.Status500InternalServerError;
        var errors = new List<string> { ex.Message };

        if (ex.InnerException != null)
        {
            errors.Add(ex.InnerException.Message);
        }

        code = ex switch
        {
            DirectoryNotFoundException => StatusCodes.Status404NotFound,
            ResourceNotFound => StatusCodes.Status404NotFound,
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            // UnprocessableRequestException => StatusCodes.Status422UnprocessableEntity,
            _ => code
        };

        var result = JsonConvert.SerializeObject(ApiResult<string>.Failure(errors));

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(result);
    }
}
