namespace Orders.Application.DTOs;

public sealed record OrderDetailItemOutput(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal Total
);
