using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Application.Contracts;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}