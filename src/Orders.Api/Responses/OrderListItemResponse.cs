namespace Orders.Api.Responses;

public record OrderListItemResponse(
    Guid Id,
    Guid CustomerId,
    decimal Total,
    int ItemsCount
);