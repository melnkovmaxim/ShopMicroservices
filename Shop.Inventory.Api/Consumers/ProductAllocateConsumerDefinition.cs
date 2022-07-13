using MassTransit;

namespace Shop.Inventory.Api.Consumers;

public class ProductAllocateConsumerDefinition: ConsumerDefinition<ProductAllocateConsumer>
{
    public ProductAllocateConsumerDefinition()
    {
        EndpointName = "allocate_product";
    }
}