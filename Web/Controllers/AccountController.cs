using Application;
using Application.UseCase;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Web.Controllers;

public class AccountController : ApplicationController
{
    
    
    [HttpGet]
    [Route("register")]
    public IActionResult Register(
        [FromBody] RegistrationRequest request,
        [FromServices] RegistrationUseCase registrationUseCase)
    {
        var user=registrationUseCase
            .Handle(new RegistrationCommand(request.Email, request.Password));
     
        return Ok(user);
    }
}