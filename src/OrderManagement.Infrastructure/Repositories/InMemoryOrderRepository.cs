using OrderManagement.Application.Contracts;
using OrderManagement.Application.Exceptions;
using OrderManagement.Domain.Models.Orders;
using System.Collections.Concurrent;

namespace OrderManagement.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<Guid, Order> _orders = new();

    public Task AddAsync(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

       if (!_orders.TryAdd(order.OrderId, order))
       {
            throw new DuplicateOrderException(order.OrderId);
       }

        return Task.CompletedTask;
    }
}
