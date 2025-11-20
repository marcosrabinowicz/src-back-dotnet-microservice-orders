using Microsoft.EntityFrameworkCore;
using Orders.Application.Interfaces;
using Orders.Domain.Aggregates;
using Orders.Infrastructure.EF;

namespace Orders.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrdersDbContext _db;

    public OrderRepository(OrdersDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Order order, CancellationToken ct)
    {
        await _db.Orders.AddAsync(order, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, ct);
    }
}