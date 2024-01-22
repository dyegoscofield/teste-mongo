using System.Diagnostics.CodeAnalysis;

namespace TesteMongoDb.Api.Middlewares;

[ExcludeFromCodeCoverage]
public class ExceptionMiddleware
{
    const string DEFAULT_EXCEPTION = "Ocorreu um erro inesperado.";
    const string CANCELED_EXCEPTION = "A solicitacao foi cancelada.";

    readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();
            await _next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An unexpected error occurred.");

            switch (exception)
            {
                case OperationCanceledException:
                    await HandlingExceptionAsync(context, StatusCodes.Status400BadRequest, CANCELED_EXCEPTION);
                    break;
                case ArgumentNullException:
                    await HandlingExceptionAsync(context, StatusCodes.Status400BadRequest, exception.Message);
                    break;
                case ArgumentException:
                    await HandlingExceptionAsync(context, StatusCodes.Status400BadRequest, exception.Message);
                    break;
                case ApplicationException:
                    await HandlingExceptionAsync(context, StatusCodes.Status400BadRequest, exception.Message);
                    break;
                default:
                    await HandlingExceptionAsync(context, StatusCodes.Status500InternalServerError, DEFAULT_EXCEPTION);
                    break;
            }
        }
    }

    private static Task HandlingExceptionAsync(HttpContext context,
        int statusCodes,
        string error)
    {
        context.Response.StatusCode = statusCodes;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsJsonAsync(new ErrorModel(new ErrorModel.ErrorDetails()
        {
            Message = error,
            StatusCode = statusCodes
        }));
    }
}