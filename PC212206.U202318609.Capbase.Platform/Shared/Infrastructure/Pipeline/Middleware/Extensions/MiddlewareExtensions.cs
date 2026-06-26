using PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;

namespace PC212206.U202318609.Capbase.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
///     Middleware extensions
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    ///     Registers the global exception handler middleware in the HTTP pipeline.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The configured application builder.</returns>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
