using KalaConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KalaConnect.Infrastructure.Persistence;

public class KalaConnectDbContext : DbContext
{
    public KalaConnectDbContext(
        DbContextOptions<KalaConnectDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.FullName)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();

            entity.HasIndex(x => x.Email)
                .IsUnique();

            entity.Property(x => x.CreatedAt)
                .IsRequired();

            entity.Property(x => x.IsActive)
                .IsRequired();
        });
    }
}