namespace Orders.Application.DTOs;

public record OrderListItemOutput(
    Guid Id,
    Guid CustomerId,
    decimal Total,
    int ItemsCount
);
