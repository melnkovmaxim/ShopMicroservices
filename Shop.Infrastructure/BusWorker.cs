using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Shop.Infrastructure;

public class BusWorker: IHostedService
{
    private readonly IBusControl _bus;

    public BusWorker(IBusControl bus)
    {
        _bus = bus;
    }

    public Task StartAsync(CancellationToken cancellationToken) => _bus.StartAsync(cancellationToken);

    public Task StopAsync(CancellationToken cancellationToken) => _bus.StopAsync(cancellationToken);
}