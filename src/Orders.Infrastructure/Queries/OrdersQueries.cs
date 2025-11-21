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
}
