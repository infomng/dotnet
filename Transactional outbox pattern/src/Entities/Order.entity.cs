namespace Entities;

public class Order
{
    public Guid Id { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}