using Orders.Application.DTOs;

namespace Orders.Application.UseCases;

public interface ICreateOrderUseCase
{
    Task<CreateOrderOutput> ExecuteAsync(CreateOrderInput input, CancellationToken ct);
}