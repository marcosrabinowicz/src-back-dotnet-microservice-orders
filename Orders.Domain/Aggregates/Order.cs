using Orders.Domain.Entities;
using Orders.Domain.Exceptions;

namespace Orders.Domain.Aggregates;

public class Order
{
    private readonly List<OrderItem> _items = new();

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public decimal Total => _items.Sum(i => i.Total);

    private Order() { } // EF Core

    public Order(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new DomainException("CustomerId inválido.");

        CustomerId = customerId;
    }

    public void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        var item = new OrderItem(productId, quantity, unitPrice);
        _items.Add(item);

        EnsureInvariants();
    }

    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item == null)
            throw new DomainException("Item inexistente.");

        _items.Remove(item);

        EnsureInvariants();
    }

    private void EnsureInvariants()
    {
        if (!_items.Any())
            throw new DomainException("Pedido deve ter pelo menos 1 item.");

        if (Total < 0)
            throw new DomainException("Total inválido.");
    }
}
