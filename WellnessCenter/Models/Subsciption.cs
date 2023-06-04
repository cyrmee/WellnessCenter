using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellnessCenter.Models;

public class Subscription
{
    public int Id { get; set; }
    public required string PaymentAmount { get; set; }
    public required string PaymentStatus { get; set; }
    public required int ServiceId { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
    public required DateTime SubscriptionDate { get; set; } = DateTime.Now;
    public required int CustomerId { get; set; }

    // Service Given By, Employee
    public required string EmployeeUserName { get; set; }

    public Customer Customer { get; set; } = default!;
    public Service Service{ get; set; } = default!;
    public Employee Employee { get; set; } = default!;
}