namespace Orders.Api.Requests;

public record CreateOrderItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice
);