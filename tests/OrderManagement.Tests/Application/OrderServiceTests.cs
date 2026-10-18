using NSubstitute;
using OrderManagement.Application.Contracts;
using OrderManagement.Application.Services;

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
}