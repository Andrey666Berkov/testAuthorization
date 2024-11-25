using Application;
using Application.UseCase;
using Application.UseCase.LoginUseCase;
using Application.UseCase.RegistrationUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Error;
using Web.Extensions;

namespace Web.Controllers;

public class AccountController : ApplicationController
{
    
    
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register(
        [FromBody] RegistrationRequest request,
        [FromServices] RegistrationUseCase registrationUseCase)
    {
        var user=await registrationUseCase.Handle(
            new RegistrationCommand(
                request.Name,
                request.Email, 
                request.Password));
     
        return Ok(user);
    }
    
    [HttpPost]
    [Route("authorization")]
    public async Task<ActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginUseCase loginUseCase)
    {
        var user=await loginUseCase.Handle(
            new LoginCommand(
                request.Email,
                request.Password));
        if (user.IsFailure)
            return user.Error.ToError();
        
        return Ok(user.Value);
    }
    
    [Authorize]
    [HttpGet("check")]
    public async Task<IActionResult> Check()
    {
        return Ok();
    }
}