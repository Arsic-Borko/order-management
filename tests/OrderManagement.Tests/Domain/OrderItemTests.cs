using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Tests.Domain;

[TestFixture]
public class OrderItemTests
{
    [Test]
    public void InitializeOrderItem_WithValidParameters_ShouldCreateOrderItem()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = 1;
        var unitPrice = 20.0m;

        // Act
        var orderItem = new OrderItem(productId, quantity, unitPrice);

        // Assert
        Assert.That(orderItem.ProductId, Is.EqualTo(productId));
        Assert.That(orderItem.Quantity, Is.EqualTo(quantity));
        Assert.That(orderItem.UnitPrice, Is.EqualTo(unitPrice));
    }

    [TestCase(1, "10.00", "10.00")]
    [TestCase(3, "19.99", "59.97")]
    [TestCase(172, "21.22", "3649.84")]
    public void InitializeOrderItem_WithValidParameters_ShouldCalculateLineTotal(
        int quantity,
        string unitPrice,
        string expectedTotal)
    {
        // Arrange
        var productId = Guid.NewGuid();
        var price = decimal.Parse(
            unitPrice,
            System.Globalization.CultureInfo.InvariantCulture);

        var expected = decimal.Parse(
            expectedTotal,
            System.Globalization.CultureInfo.InvariantCulture);

        // Act
        var orderItem = new OrderItem(productId, quantity, price);

        // Assert
        Assert.That(orderItem.LineTotal, Is.EqualTo(expected));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-10)]
    public void InitializeOrderItem_WithInvalidQuantity_ShouldThrowException(
    int quantity)
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        void Act() => new OrderItem(productId, quantity, 20m);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }

    [Test]
    public void InitializeOrderItem_WithInvalidPrice_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = 1;
        var unitPrice = -1m;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new OrderItem(productId, quantity, unitPrice));

        Assert.That(exception.ParamName, Is.EqualTo("unitPrice"));
    }

    [Test]
    public void InitializeOrderItem_WithInvalidProductId_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.Empty;
        var quantity = 1;
        var unitPrice = 10.00m;

        // Act
        var exception = Assert.Throws<ArgumentException>(() =>
            new OrderItem(productId, quantity, unitPrice));

        // Assert
        Assert.That(exception.ParamName, Is.EqualTo("productId"));
    }
}
