namespace Orders.Application.DTOs;

public record CreateOrderItemInput(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);
