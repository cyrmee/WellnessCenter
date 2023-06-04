using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WellnessCenter.Models;

public class Employee : IdentityUser
{
    public async Task<IdentityResult> GenerateUserIdentityAsync(UserManager<Employee> manager)
    {
        return await manager.CreateAsync(this);
    }

    public ICollection<Subscription> Subscriptions { get; set; } = default!;
}
