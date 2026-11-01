using OrderManagement.Application.Exceptions;
using OrderManagement.Domain.Models.Orders;
using OrderManagement.Infrastructure.Repositories;

namespace OrderManagement.Tests.Infrastructure;

[TestFixture]
public class InMemoryOrderRepositoryTests
{
    private InMemoryOrderRepository _repository;

    [SetUp]
    public void Setup()
    {
        _repository = new InMemoryOrderRepository();
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
}
