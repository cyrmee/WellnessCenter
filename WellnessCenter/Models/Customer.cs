using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WellnessCenter.Models;

public class Customer
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required string Gender { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
    public required DateTime DateOfBirth { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; } = default!;
}
