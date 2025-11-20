using Orders.Domain.Exceptions;

namespace Orders.Domain.Entities;

public sealed class OrderItem
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Total => Quantity * UnitPrice;

    private OrderItem() { } // EF Core

    public OrderItem(Guid productId, int quantity, decimal unitPrice)
    {
        if (productId == Guid.Empty)
            throw new DomainException("ProductId inválido.");
        if (quantity <= 0)
            throw new DomainException("Quantidade inválida.");
        if (unitPrice <= 0)
            throw new DomainException("UnitPrice inválido.");

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
