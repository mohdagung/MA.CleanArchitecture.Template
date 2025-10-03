using Microsoft.EntityFrameworkCore;
using MA.Clean.Template.Domain.Entities;

namespace MA.Clean.Template.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Sample> Samples => Set<Sample>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sample>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
        });
    }
}