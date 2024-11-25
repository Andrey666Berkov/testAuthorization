namespace Web.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseErrorMyMiidlware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorMyMiiddleware>();
    }
}