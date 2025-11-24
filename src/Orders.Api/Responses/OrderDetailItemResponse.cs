namespace Orders.Api.Responses;

public sealed record OrderDetailItemResponse(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal Total
);
