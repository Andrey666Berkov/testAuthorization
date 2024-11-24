using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCase;

public class RegistrationUseCase
{
    private readonly UserManager<User> _userManger;

    public RegistrationUseCase(UserManager<User> userManger)
    {
        _userManger = userManger;
    }
    public void Handle(RegistrationCommand registrationCommand)
    {
        var user = _userManger.FindByEmailAsync(registrationCommand.Email);
        if(user.IsFaulted)
            return 
    }
}