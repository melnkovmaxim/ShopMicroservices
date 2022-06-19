using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Infrastructure.Authentication;

namespace Shop.Gateway.Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UserController: ControllerBase
{
    private readonly IBusControl _bus;
    private readonly IRequestClient<UserLoginCommand> _loginClient;

    public UserController(IBusControl bus, IRequestClient<UserLoginCommand> loginClient)
    {
        _bus = bus;
        _loginClient = loginClient;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateCommand command)
    {
        var uri = new Uri("rabbitmq://localhost/add_user");
        var endpoint = await _bus.GetSendEndpoint(uri);

        await endpoint.Send(command);

        return Accepted();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
    {
        var result = await _loginClient.GetResponse<JwtToken>(command);

        return Ok(result.Message);
    }
}