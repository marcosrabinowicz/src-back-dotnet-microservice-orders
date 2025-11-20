namespace Orders.Api.Requests;

public record CreateOrderRequest(
    Guid CustomerId,
    List<CreateOrderItemRequest> Items
);