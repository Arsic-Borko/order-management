using NSubstitute;
using OrderManagement.Application.Contracts;
using OrderManagement.Application.Services;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Tests.Application;

[TestFixture]
public class OrderServiceTests
{
    private IOrderRepository _orderRepository;
    private IOrderService _orderService;

    [SetUp]
    public void SetUp()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _orderService = new OrderService(_orderRepository);
    }

    [Test]
    public async Task CreateOrderAsync_WhenCalled_ShouldReturnValidOrderId()
    {
        // Arrange & Act
        var orderId = await _orderService.CreateOrderAsync();

        // Assert
        Assert.That(orderId, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public async Task CreateOrderAsync_WhenCalled_ShouldAddOrderToRepository()
    {
        // Act
        var orderId = await _orderService.CreateOrderAsync();

        // Assert
        await _orderRepository.Received(1).AddAsync(
            Arg.Is<Order>(order => order.OrderId == orderId));
    }
}