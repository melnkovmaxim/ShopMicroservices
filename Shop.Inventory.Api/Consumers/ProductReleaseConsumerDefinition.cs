using MassTransit;

namespace Shop.Inventory.Api.Consumers;

public class ProductReleaseConsumerDefinition: ConsumerDefinition<ProductReleaseConsumer>
{
    public ProductReleaseConsumerDefinition()
    {
        EndpointName = "release_product";
    }
}