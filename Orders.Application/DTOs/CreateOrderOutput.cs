namespace Orders.Application.DTOs;

public record CreateOrderOutput(
    Guid OrderId,
    decimal Total
);
