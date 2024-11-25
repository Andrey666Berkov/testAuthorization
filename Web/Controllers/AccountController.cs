using Account.Application;
using Account.Application.UseCase;
using Account.Application.UseCase.LoginUseCase;
using Account.Application.UseCase.RefreshTokenUseCase;
using Account.Application.UseCase.RegistrationUseCase;
using Account.Domain.Models;
using Account.Infrastructure;
using Account.Infrastructure.Permission;
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
    
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshTokenRequest request,
        [FromServices] RefreshTokenUseCase refreshUseCase)
    {
        return Ok();
    }
    
    [HttpPost]
    [Route("authorization")]
    public async Task<ActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginUseCase loginUseCase,
        CancellationToken cancellationToken = default)
    {
        var user=await loginUseCase.Handle(
            new LoginCommand(request.Email, request.Password), cancellationToken);
        if (user.IsFailure)
            return user.Error.ToError();
        
        return Ok(user.Value);
    }
    
    [Permission("userRole")]
    [HttpGet("check")]
    public async Task<IActionResult> Check()
    {
        return Ok();
    }
}