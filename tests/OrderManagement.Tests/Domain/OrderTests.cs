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

    [Test]
    public void TotalPrice_CreateEmptyOrder_TotalPriceShouldBeZero()
    {
        // Arrange
        var order = CreateOrder();

        // Act & Assert
        Assert.That(order.TotalPrice, Is.EqualTo(0m));
    }

    [Test]
    public void TotalPrice_WhenOrderHasOneItem_ShouldReturnItemTotal()
    {
        // Arrange
        var order = CreateOrder();
        var item = CreateOrderItem();
        order.AddItem(item);

        // Act
        var initialTotal = order.TotalPrice;

        // Assert
        Assert.That(initialTotal, Is.EqualTo(10m));
    }

    [Test]
    public void TotalPrice_CreateOrderWithManyOrderItems_TotalPriceShouldBeCorrectlyCalculated()
    {
        // Arrange
        var order = CreateOrder();
        var item1 = CreateOrderItem(2, 100m);
        var item2 = CreateOrderItem(1, 50m);
        var item3 = CreateOrderItem(3, 20m);
        order.AddItem(item1);
        order.AddItem(item2);
        order.AddItem(item3);

        // Act
        var initialTotal = order.TotalPrice;

        // Assert
        Assert.That(initialTotal, Is.EqualTo(310m));
    }

    [Test]
    public void TotalPrice_WhenItemIsAddedAfterInitialRead_ShouldReflectUpdatedTotal()
    {
        // Arrange
        var order = CreateOrder();
        var item1 = CreateOrderItem();
        var item2 = CreateOrderItem();

        // Act & Assert
        order.AddItem(item1);
        var initialTotal = order.TotalPrice;

        Assert.That(initialTotal, Is.EqualTo(10m));

        order.AddItem(item2);
        var updatedTotal = order.TotalPrice;

        Assert.That(updatedTotal, Is.EqualTo(20m));
    }

    private OrderItem CreateOrderItem(int quantity = 1, decimal unitPrice = 10m)
        => new OrderItem(Guid.NewGuid(), quantity, unitPrice);

    private Order CreateOrder() => new Order(Guid.NewGuid());
}
