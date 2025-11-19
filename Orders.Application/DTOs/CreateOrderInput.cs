namespace Orders.Application.DTOs;

public record CreateOrderInput(
    Guid CustomerId,
    List<CreateOrderItemInput> Items
);
