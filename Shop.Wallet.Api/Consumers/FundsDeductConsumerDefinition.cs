using MassTransit;

namespace Shop.Wallet.Api.Consumers;

public class FundsDeductConsumerDefinition: ConsumerDefinition<FundsDeductConsumer>
{
    public FundsDeductConsumerDefinition()
    {
        EndpointName = "deduct_funds";
    }
}