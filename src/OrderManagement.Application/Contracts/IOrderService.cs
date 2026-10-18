namespace OrderManagement.Application.Contracts;

public interface IOrderService
{
    public Task<Guid> CreateOrderAsync();
}
