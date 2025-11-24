namespace Orders.Application.DTOs;

public sealed record OrderDetailOutput(
    Guid Id,
    Guid CustomerId,
    decimal Total,
    IReadOnlyCollection<OrderDetailItemOutput> Items
);
