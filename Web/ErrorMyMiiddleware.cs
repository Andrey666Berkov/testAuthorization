using System.Net;
using Shared;
using Shared.Error;

namespace Web;

public class ErrorMyMiiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMyMiiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            var error=ErrorMy.Failure(ex.Message, "System");
            var envelopes = Envelope.Error(error);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsJsonAsync(envelopes);
        }
    }
}