using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace recipe_catalog_api.Config;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
  private readonly ILogger<GlobalExceptionHandler> _logger = logger;

  public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken
  )
  {
    _logger.LogError(exception, "Unhandled exception processing {Method} {Path}",
      httpContext.Request.Method,
      httpContext.Request.Path
    );

    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    httpContext.Response.ContentType = "application/json";

    await httpContext.Response.WriteAsJsonAsync(
      new ProblemDetails
      {
        Status = StatusCodes.Status500InternalServerError,
        Title = "Internal server error",
        Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1"
      },
      cancellationToken);

    return true;
  }
}
