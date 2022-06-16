using MassTransit;
using Microsoft.AspNetCore.Identity;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.User.Api.Entities;

namespace Shop.Product.Api.Consumers;

public class UserLoginConsumer: IConsumer<UserLoginCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserLoginConsumer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task Consume(ConsumeContext<UserLoginCommand> context)
    {
        var user = await _userManager.FindByNameAsync(context.Message.Username);
        var isSucceeded = await _userManager.CheckPasswordAsync(user, context.Message.Password);
        
        if (isSucceeded == false)
        {
            throw new Exception("Неверный логин или пароль");
        }

        var result = new UserCreatedEvent()
        {
            Email = user.Email,
            Password = user.PasswordHash,
            Username = user.UserName,
            UserId = user.Id.ToString(),
            PhoneNumber = user.PhoneNumber
        };

        await context.RespondAsync(result);
    }
}