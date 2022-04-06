using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MoneyKeeper.Domain.Data.Models;

namespace MoneyKeeper.Data;

#nullable disable

public sealed class MoneyKeeperContext : DbContext
{
    public MoneyKeeperContext(DbContextOptions<MoneyKeeperContext> options) : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Expense>();

        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("now()")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        base.OnModelCreating(modelBuilder);
    }
}
