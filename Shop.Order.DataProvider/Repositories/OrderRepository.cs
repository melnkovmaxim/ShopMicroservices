using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Shop.Order.DataProvider.Repositories;

using Infrastructure.Order;

public class OrderRepository: IOrderRepository
{
    private readonly IMongoCollection<Order> _mongoOrders;

    public OrderRepository(IMongoDatabase mongo)
    {
        _mongoOrders = mongo.GetCollection<Order>("order");
    }
    
    public Task<Order> GetOrderAsync(string orderId)
    {
        return _mongoOrders.AsQueryable().FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    // TODO: в таску завернуть
    public IEnumerable<Order> GetAllUserOrdersAsync(string userId)
    {
        return _mongoOrders.AsQueryable().Where(o => o.UserId == userId).ToList();
    }

    public Task CreateOrderAsync(Order order)
    {
        return _mongoOrders.InsertOneAsync(order);
    }
}