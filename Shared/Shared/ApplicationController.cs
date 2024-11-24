using Microsoft.AspNetCore.Mvc;
using Shared.Error;

namespace Shared;

[ApiController]
[Route("[controller]")]
public class ApplicationController :ControllerBase
{
    public override OkObjectResult Ok(object obj)
    {
        var envelope=Envelope.Ok(obj);
        return new (envelope);
    }
}