using Microsoft.EntityFrameworkCore;
using Orders.Application.DTOs;
using Orders.Application.Interfaces;
using Orders.Infrastructure.EF;

namespace Orders.Infrastructure.Queries;

public class OrdersQueries : IOrderQuery
{
    private readonly OrdersDbContext _db;

    public OrdersQueries(OrdersDbContext db)
    {
        _db = db;
    }

    public async Task<List<OrderListItemOutput>> ListAsync(int page, int pageSize, CancellationToken ct)
    {
        return await _db.Orders
            .AsNoTracking()
            .OrderByDescending(o => o.Id)
            .Select(o => new OrderListItemOutput(
                o.Id,
                o.CustomerId,
                o.Total,
                o.Items.Count
            ))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<OrderDetailOutput?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return _db.Orders
            .AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new OrderDetailOutput(
                o.Id,
                o.CustomerId,
                o.Total,
                o.Items.Select(i => new OrderDetailItemOutput(
                    i.ProductId,
                    i.Quantity,
                    i.UnitPrice,
                    i.Total
                )).ToList()
            ))
            .FirstOrDefaultAsync(ct);
    }
}
