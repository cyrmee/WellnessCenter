using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WellnessCenter.Models;

namespace WellnessCenter.Data;

public class ApplicationDbContext : IdentityDbContext<Employee>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
                .HasMany(e => e.Subscriptions)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        modelBuilder.Entity<Service>()
                .HasMany(e => e.Subscriptions)
                .WithOne(e => e.Service)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        modelBuilder.Entity<Employee>()
                .HasMany(e => e.Subscriptions)
                .WithOne(e => e.Employee)
                .HasPrincipalKey(e => e.UserName)
                .HasForeignKey(e => e.EmployeeUserName)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
    }

    public DbSet<WellnessCenter.Models.Customer> Customer { get; set; } = default!;

    public DbSet<WellnessCenter.Models.Service> Service { get; set; } = default!;

    public DbSet<WellnessCenter.Models.Subscription> Subscription { get; set; } = default!;
}
