using OrderManagement.Application.Contracts;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> CreateOrderAsync()
    {
        var orderId = Guid.NewGuid();
        var order = new Order(orderId);

        await _orderRepository.AddAsync(order);

        return orderId;
    }
}
