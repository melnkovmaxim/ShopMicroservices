using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Domain.Queries;
using Shop.Gateway.Api.Controllers;
using Xunit;

namespace Shop.Gateway.Api.Tests;

public class ProductControllerTests
{
    private readonly IBusControl _busControl;

    public ProductControllerTests()
    {
        var sendEndpoint = new Mock<ISendEndpoint>();
        var busControl = new Mock<IBusControl>();

        busControl.Setup(x => x.GetSendEndpoint(It.IsAny<Uri>()))
            .ReturnsAsync(sendEndpoint.Object);

        _busControl = busControl.Object;
    }
    
    [Fact]
    async Task AddProduct_CommandIsNull_ShouldReturnAccepted202()
    {
        // Arrange
        var productController = new ProductsController(_busControl, null);
        
        // Act
        var result = await productController.AddProduct(It.IsAny<ProductCreateCommand>());

        // Assert
        var acceptedResult = result as AcceptedResult;

        Assert.True(acceptedResult?.StatusCode == StatusCodes.Status202Accepted);
    }

    [Fact]
    public async Task GetProduct_MockBus_ShouldReturnOkWithCorrectProduct()
    {
        // Arrange
        var requestClient = new Mock<IRequestClient<GetProductByIdQuery>>();
        var fakeProduct = new Mock<Response<ProductCreatedEvent>>();
        var productController = new ProductsController(_busControl, requestClient.Object);
        var productCreatedEvent = new ProductCreatedEvent() { ProductId = "123" };
        
        fakeProduct.Setup(x => x.Message)
            .Returns(productCreatedEvent);
        
        requestClient.Setup(x 
                => x.GetResponse<ProductCreatedEvent>(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>(),It.IsAny<RequestTimeout>()))
            .ReturnsAsync(fakeProduct.Object);

        
        // Act
        var result = await productController.GetProduct(productCreatedEvent.ProductId);
        
        // Assert
        var okResult = result as OkObjectResult;
        var receivedProduct = okResult?.Value as ProductCreatedEvent;
        
        Assert.True(okResult?.StatusCode == StatusCodes.Status200OK);
        Assert.True(receivedProduct?.ProductId == productCreatedEvent.ProductId);
    }
}