using MassTransit;
using Microsoft.AspNetCore.Identity;
using Shop.Domain.Commands;
using Shop.User.Api.Entities;

namespace Shop.User.Api.Consumers;

public class UserCreateConsumer: IConsumer<UserCreateCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserCreateConsumer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task Consume(ConsumeContext<UserCreateCommand> context)
    {
        var message = context.Message;
        var user = new ApplicationUser()
        {
            UserName = message.Username,
            Email = message.Email,
            PhoneNumber = message.PhoneNumber
        };
        
        var identityResult = await _userManager.CreateAsync(user, message.Password);

        if (identityResult.Succeeded == false)
        {
            throw new Exception("Произошла ошибка при регистрации");
        }
    }
}