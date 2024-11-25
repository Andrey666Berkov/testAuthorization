using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Error;

namespace Web.Extensions;

public static class ErrorExtansions
{
    public static OkObjectResult ToError(this ErrorMy errorMy)
    {
        var envelope=Envelope.Error(errorMy);
        return new (envelope);
    }
}