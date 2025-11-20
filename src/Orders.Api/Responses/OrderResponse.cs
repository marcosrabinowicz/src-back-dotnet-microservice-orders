namespace Orders.Api.Responses;

public record OrderResponse(
    Guid Id,
    Guid CustomerId,
    decimal Total,
    IEnumerable<OrderItemResponse> Items
);