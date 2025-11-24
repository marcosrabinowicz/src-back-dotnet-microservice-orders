using Orders.Application.DTOs;

namespace Orders.Application.Interfaces;

public interface IOrderQuery
{
    Task<List<OrderListItemOutput>> ListAsync(int page, int pageSize, CancellationToken ct);
    Task<OrderDetailOutput?> GetByIdAsync(Guid id, CancellationToken ct);
}
