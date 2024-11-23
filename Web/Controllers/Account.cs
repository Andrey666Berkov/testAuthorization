using Application;
using Application.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class Account : Controller
{
    [HttpGet]
    
    public IActionResult Register(
        [FromBody] LoginRequest request,
        [FromServices] RegistrationUseCase registrationUseCase)
    {
        
         registrationUseCase.Handle(new LoginCommand(request.Email, request.Password));
        return Ok();
    }
}