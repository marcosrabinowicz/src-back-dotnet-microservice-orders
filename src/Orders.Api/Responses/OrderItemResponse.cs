namespace Orders.Api.Responses;

public record OrderItemResponse(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal Total
);