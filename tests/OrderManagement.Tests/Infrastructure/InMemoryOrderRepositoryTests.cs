using OrderManagement.Application.Exceptions;
using OrderManagement.Domain.Models.Orders;
using OrderManagement.Infrastructure.Repositories;

namespace OrderManagement.Tests.Infrastructure;

[TestFixture]
public class InMemoryOrderRepositoryTests
{
    private InMemoryOrderRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        _repository = new InMemoryOrderRepository();
    }

    [Test]
    public async Task AddAsync_WhenOrderIsValid_ShouldStoreOrder()
    {
        // Arrange
        var order = new Order(Guid.NewGuid());

        // Act
        await _repository.AddAsync(order);

        // Assert
        var storedOrder = await _repository.GetByIdAsync(order.OrderId);

        Assert.That(storedOrder, Is.SameAs(order));
    }

    [Test]
    public async Task AddAsync_WhenOrderIdAlreadyExists_ShouldThrowDuplicateOrderException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var firstOrder = new Order(orderId);
        var secondOrder = new Order(orderId);

        await _repository.AddAsync(firstOrder);

        // Act & Assert
        Assert.ThrowsAsync<DuplicateOrderException>(
            async () => await _repository.AddAsync(secondOrder));
    }

    [Test]
    public void AddAsync_WhenOrderIsNull_ShouldThrowArgumentNullException()
    {
        Assert.ThrowsAsync<ArgumentNullException>(
            async () => await _repository.AddAsync(null!));
    }

    [Test]
    public async Task GetByIdAsync_WhenOrderDoesNotExist_ShouldReturnNull()
    {
        // Act
        var result = await _repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.Null);
    }
}