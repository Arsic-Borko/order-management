namespace OrderManagement.Application.Exceptions;

public sealed class DuplicateOrderException : Exception
{
    public DuplicateOrderException(Guid orderId)
        : base($"Order with ID '{orderId}' already exists.")
    {
    }
}
