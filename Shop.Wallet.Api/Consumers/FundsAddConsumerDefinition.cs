using MassTransit;

namespace Shop.Wallet.Api.Consumers;

public class FundsAddConsumerDefinition: ConsumerDefinition<FundsAddConsumer>
{
    public FundsAddConsumerDefinition()
    {
        EndpointName = "add_funds";
    }
}