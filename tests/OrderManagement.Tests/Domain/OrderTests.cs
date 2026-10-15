using OrderManagement.Domain.Models.Enums;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Tests.Domain;

[TestFixture]
public class OrderTests
{
    [Test]
    public void InitializeOrder_WithValidParameters_ShouldCreateOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        // Act
        var order = new Order(orderId);

        // Assert
        Assert.That(order, Is.Not.Null);
        Assert.That(order.OrderId, Is.EqualTo(orderId));
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Pending));
        Assert.That(order.Items, Is.Empty);
    }

    [Test]
    public void Constructor_WhenOrderIdIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange & Act
        void Act() => new Order(Guid.Empty);

        // Assert
        var exception = Assert.Throws<ArgumentException>(Act);
        Assert.That(exception!.ParamName, Is.EqualTo("orderId"));
    }

    [Test]
    public void AddItem_WhenOrderItemIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var order = CreateOrder();

        // Act & Assert
        void Act() => order.AddItem(null!);
        var exception = Assert.Throws<ArgumentNullException>(Act);
        
        Assert.That(exception!.ParamName, Is.EqualTo("item"));
        Assert.That(order.Items, Is.Empty);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Pending));
    }

    [Test]
    public void AddItem_WhenOrderIsPending_ShouldAddItem()
    {
        // Arrange
        var order = CreateOrder();
        var item = CreateOrderItem();

        // Act
        order.AddItem(item);

        // Assert
        Assert.That(order.Items, Has.Count.EqualTo(1));
        Assert.That(order.Items.Single(), Is.SameAs(item));
    }

    [Test]
    public void AddItem_WhenOrderIsConfirmed_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var order = CreateOrder();
        order.AddItem(CreateOrderItem());
        order.Confirm();

        // Act
        void Act() => order.AddItem(CreateOrderItem());

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Confirmed));
        Assert.That(order.Items, Has.Count.EqualTo(1));
    }

    [Test]
    public void AddItem_WhenOrderIsCancelled_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var order = CreateOrder();
        order.Cancel();

        // Act
        void Act() => order.AddItem(CreateOrderItem());

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Cancelled));
        Assert.That(order.Items, Is.Empty);
    }

    [Test]
    public void Confirm_WhenOrderHasNoItems_ShouldThrowAndRemainPending()
    {
        // Arrange
        var order = CreateOrder();

        // Act
        void Act() => order.Confirm();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Pending));
    }

    [Test]
    public void Confirm_WhenOrderIsPendingAndHasItems_ShouldSetStatusToConfirmed()
    {
        // Arrange
        var order = CreateOrder();
        order.AddItem(CreateOrderItem());

        // Act
        order.Confirm();

        // Assert
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Confirmed));
    }

    [Test]
    public void Confirm_WhenOrderIsAlreadyConfirmed_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var order = CreateOrder();
        order.AddItem(CreateOrderItem());
        order.Confirm();

        // Act
        void Act() => order.Confirm();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Confirmed));
    }

    [Test]
    public void Confirm_WhenOrderIsAlreadyCancelled_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var order = CreateOrder();
        order.Cancel();

        // Act
        void Act() => order.Confirm();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Cancelled));
    }

    [Test]
    public void Cancel_WhenOrderIsPending_ShouldSetStatusToCancelled()
    {
        // Arrange
        var order = CreateOrder();

        // Act
        order.Cancel();

        // Assert
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Cancelled));
    }

    [Test]
    public void Cancel_WhenOrderIsAlreadyConfirmed_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var order = CreateOrder();
        order.AddItem(CreateOrderItem());
        order.Confirm();

        // Act
        void Act() => order.Cancel();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Confirmed));
    }

    [Test]
    public void Cancel_WhenOrderIsAlreadyCancelled_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var order = CreateOrder();
        order.Cancel();

        // Act
        void Act() => order.Cancel();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Cancelled));
    }

    private OrderItem CreateOrderItem() => new OrderItem(Guid.NewGuid(), 1, 10m);
    private Order CreateOrder() => new Order(Guid.NewGuid());
}
