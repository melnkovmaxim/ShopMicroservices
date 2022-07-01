using MassTransit;
using Microsoft.AspNetCore.Identity;
using Shop.Domain.Commands;
using Shop.Domain.Entities;
using Shop.Infrastructure.Authentication;

namespace Shop.User.Query.Api.Consumers;

public class UserLoginConsumer: IConsumer<UserLoginCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthenticationService _authenticationService;

    public UserLoginConsumer(UserManager<ApplicationUser> userManager, IAuthenticationService authenticationService)
    {
        _userManager = userManager;
        _authenticationService = authenticationService;
    }
    
    public async Task Consume(ConsumeContext<UserLoginCommand> context)
    {
        var user = await _userManager.FindByNameAsync(context.Message.Username);
        var isSucceeded = await _userManager.CheckPasswordAsync(user, context.Message.Password);
        
        if (isSucceeded == false)
        {
            throw new Exception("Неверный логин или пароль");
        }

        var jwtToken = _authenticationService.CreateJwt(user.Id);

        await context.RespondAsync<JwtToken>(jwtToken);
    }
}