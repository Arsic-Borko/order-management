using OrderManagement.Application.Contracts;
using OrderManagement.Application.Services;
using OrderManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();

var app = builder.Build();

app.MapPost("/orders", async (IOrderService orderService) =>
{
    var orderId = await orderService.CreateOrderAsync();

    return Results.Created($"/orders/{orderId}", new
    {
        OrderId = orderId
    });
});

app.MapGet("/orders/{orderId:guid}",
    async (Guid orderId, IOrderRepository repository) =>
    {
        var order = await repository.GetByIdAsync(orderId);

        return order is null
            ? Results.NotFound()
            : Results.Ok(new
            {
                order.OrderId,
                order.Status,
                order.TotalPrice
            });
    });

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();
