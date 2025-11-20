using Microsoft.AspNetCore.Mvc;
using Orders.Api.Requests;
using Orders.Api.Responses;
using Orders.Application.DTOs;
using Orders.Application.Interfaces;
using Orders.Application.UseCases;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _createOrder;
    private readonly IOrderRepository _repository;

    public OrdersController(CreateOrderUseCase createOrder, IOrderRepository repo)
    {
        _createOrder = createOrder;
        _repository = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request, CancellationToken ct)
    {
        var input = new CreateOrderInput(
            request.CustomerId,
            request.Items.Select(i => new CreateOrderItemInput(i.ProductId, i.Quantity, i.UnitPrice)).ToList()
        );

        var result = await _createOrder.ExecuteAsync(input, ct);

        return CreatedAtAction(nameof(GetById), new { id = result.OrderId }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var order = await _repository.GetByIdAsync(id, ct);

        if (order == null) return NotFound();

        var response = new OrderResponse(
            order.Id,
            order.CustomerId,
            order.Total,
            order.Items.Select(i => new OrderItemResponse(i.ProductId, i.Quantity, i.UnitPrice, i.Total))
        );

        return Ok(response);
    }
}
