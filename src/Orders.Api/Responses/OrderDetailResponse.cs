namespace Orders.Api.Responses;

public sealed record OrderDetailResponse(
    Guid Id,
    Guid CustomerId,
    decimal Total,
    IReadOnlyCollection<OrderDetailItemResponse> Items
);
