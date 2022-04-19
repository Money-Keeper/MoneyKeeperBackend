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
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Category> Categories { get; set; }

    public Task<bool> EntityExistsAsync<TEntity>(Guid id)
        where TEntity : BaseModel
    {
        return Set<TEntity>().AnyAsync(x => x.Id == id && x.DeletedAt == null);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        IEnumerable<IReadOnlyEntityType> entities = modelBuilder.Model.GetEntityTypes();

        foreach (IReadOnlyEntityType entityType in entities)
        {
            if (entityType.ClrType is not null && entityType.ClrType.IsAssignableTo(typeof(BaseModel)))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property("CreatedAt")
                    .HasDefaultValueSql("now()")
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}
