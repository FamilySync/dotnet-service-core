using Microsoft.EntityFrameworkCore;

namespace FamilySync.Core.Example.Persistence;

public class ExampleContext : DbContext
{
    public DbSet<Models.Entity.Example> Examples { get; set; }

    public ExampleContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Models.Entity.Example>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }
}