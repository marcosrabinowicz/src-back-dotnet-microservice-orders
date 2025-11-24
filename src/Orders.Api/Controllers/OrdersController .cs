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
    private readonly ICreateOrderUseCase _createOrderUseCase;
    private readonly IOrderQuery _orderQuery;

    public OrdersController(ICreateOrderUseCase createOrder, IOrderQuery orderQuery)
    {
        _createOrderUseCase = createOrder;
        _orderQuery = orderQuery;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request, CancellationToken ct)
    {
        var input = new CreateOrderInput(
            request.CustomerId,
            request.Items.Select(i =>
                new CreateOrderItemInput(
                    i.ProductId,
                    i.Quantity,
                    i.UnitPrice
                )
            ).ToList()
        );

        var response = await _createOrderUseCase.ExecuteAsync(input, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.OrderId },
            response
        );
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
    {
        var list = await _orderQuery.ListAsync(page, pageSize, ct);

        var response = list.Select(o => new OrderListItemResponse(
            o.Id,
            o.CustomerId,
            o.Total,
            o.ItemsCount
        ));

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var order = await _orderQuery.GetByIdAsync(id, ct);

        if (order == null)
            return NotFound();

        var response = new OrderResponse(
            order.Id,
            order.CustomerId,
            order.Total,
            order.Items.Select(i => new OrderItemResponse(i.ProductId, i.Quantity, i.UnitPrice, i.Total))
        );

        return Ok(response);
    }
}
