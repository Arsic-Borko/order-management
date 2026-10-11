namespace OrderManagement.Domain.Models.Orders;

public class OrderItem
{
    public Guid ProductId { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; }

    public OrderItem(Guid productId, int quantity, decimal unitPrice)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentException(
                "Product ID cannot be empty.",
                nameof(productId));
        }

        if (quantity < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                quantity,
                "Quantity must be greater than zero.");
        }

        if (unitPrice < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(unitPrice),
                unitPrice,
                "UnitPrice cannot be less than zero.");
        }

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public decimal LineTotal => UnitPrice * Quantity;
}
