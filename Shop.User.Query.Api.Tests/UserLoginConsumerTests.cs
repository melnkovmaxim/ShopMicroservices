using MassTransit.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shop.Domain.Commands;
using Shop.Domain.Entities;
using Shop.Domain.Events;
using Shop.Infrastructure.Authentication;
using Shop.User.Query.Api.Consumers;
using IAuthenticationService = Shop.Infrastructure.Authentication.IAuthenticationService;

namespace Shop.User.Query.Api.Tests;

public class UserLoginConsumerTests
{
    [Fact]
    public async Task Test()
    {
        // Arrange
        var userLoginCommand = new UserLoginCommand()
        {
            Username  = "Admin10202",
            Password = "VeryStrongPass0"
        };
        var existedUser = new ApplicationUser() { Id = Guid.NewGuid() };
        var harness = new InMemoryTestHarness();
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        var sourceToken = new JwtToken()
        {
            EncodedJwt = "i'm encoded in tests"
        };

        userManagerMock.Setup(x => x.FindByNameAsync(userLoginCommand.Username))
            .Returns(Task.FromResult(existedUser));
        userManagerMock.Setup(x => x.CheckPasswordAsync(existedUser, userLoginCommand.Password))
            .ReturnsAsync(true);

        var authenticationServiceMock = new Mock<IAuthenticationService>();

        authenticationServiceMock.Setup(x => x.CreateJwt(existedUser.Id))
            .Returns(sourceToken);

        _ = harness.Consumer(() =>new UserLoginConsumer(userManagerMock.Object, authenticationServiceMock.Object));
            
        // Act
        JwtToken token;
        
        try
        {
            await harness.Start();

            var requestClient = harness.CreateRequestClient<UserLoginCommand>();
            var response = await requestClient.GetResponse<JwtToken>(userLoginCommand);

            token = response.Message;
        }
        finally
        {
            await harness.Stop();
        }

        // Assert
        
        Assert.NotNull(token);
        Assert.True(token.EncodedJwt == sourceToken.EncodedJwt);
    }
}