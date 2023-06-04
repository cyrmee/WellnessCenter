using System.ComponentModel.DataAnnotations;

namespace WellnessCenter.Models;

public class Service
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [DataType(DataType.Currency)]
    public required double Price { get; set; }
    public required string Description { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; } = default!;
}