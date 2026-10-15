using OrderManagement.Domain.Models.Enums;

namespace OrderManagement.Domain.Models.Orders;

public class Order
{
    public Guid OrderId { get; }
    public IReadOnlyCollection<OrderItem> Items
    {
        get { return _items.AsReadOnly(); }
    }

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    
    private readonly List<OrderItem> _items = new();

    public Order(Guid orderId)
    {
        if (orderId == Guid.Empty)
        {
            throw new ArgumentException(
                "Order Id cannot be empty.",
                nameof(orderId));
        }

        OrderId = orderId;
    }

    public void AddItem(OrderItem item)
    {
        if (Status is not OrderStatus.Pending)
        {
            throw new InvalidOperationException("Order must be in Pending status.");
        }

        if (item is null)
        {
            throw new ArgumentNullException(
                 nameof(item),
                "Order item cannot be null.");
        }
        
        _items.Add(item);
    }

    public void Confirm()
    {
        if (Status is not OrderStatus.Pending)
        {
            throw new InvalidOperationException("Order must be in Pending status.");
        }

        if (Items.Count == 0)
        {
            throw new InvalidOperationException("Order cannot be empty.");
        }

        Status = OrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status is not OrderStatus.Pending)
        {
            throw new InvalidOperationException("Order must be in Pending status.");
        }

        Status = OrderStatus.Cancelled;
    }
}
