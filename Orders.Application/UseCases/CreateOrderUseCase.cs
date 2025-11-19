using Orders.Domain.Aggregates;
using Orders.Application.DTOs;
using Orders.Application.Interfaces;

namespace Orders.Application.UseCases;

public class CreateOrderUseCase
{
    private readonly IOrderRepository _repository;

    public CreateOrderUseCase(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateOrderOutput> ExecuteAsync(CreateOrderInput input, CancellationToken ct)
    {
        var order = new Order(input.CustomerId);

        foreach (var i in input.Items)
            order.AddItem(i.ProductId, i.Quantity, i.UnitPrice);

        await _repository.AddAsync(order, ct);

        return new CreateOrderOutput(order.Id, order.Total);
    }
}
