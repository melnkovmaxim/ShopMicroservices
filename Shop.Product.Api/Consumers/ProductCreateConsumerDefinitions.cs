using MassTransit;

namespace Shop.Product.Api.Consumers;

public class ProductCreateConsumerDefinitions: ConsumerDefinition<ProductCreateConsumer>
{
    public ProductCreateConsumerDefinitions()
    {
        EndpointName = "create_product";
    }
    
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ProductCreateConsumer> consumerConfigurator)
    {
        endpointConfigurator.PrefetchCount = 16;
        endpointConfigurator.UseMessageRetry(rconfig => { rconfig.Interval(2, 100); });
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator);
    }
}