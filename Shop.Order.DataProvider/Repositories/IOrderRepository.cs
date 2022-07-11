namespace Shop.Order.DataProvider.Repositories;

using Infrastructure.Order;

public interface IOrderRepository
{
    Task<Order> GetOrderAsync(string orderId);
    IEnumerable<Order> GetAllUserOrdersAsync(string userId);
    Task CreateOrderAsync(Order order);
}