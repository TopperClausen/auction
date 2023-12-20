using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class Context : DbContext {

    public DbSet<User> users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Auction> Auctions { get; set; }

    public string DbPath { get; }
    public Context() {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "database.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Brand)
            .WithMany(b => b.Cars)
            .HasForeignKey(c => c.BrandID)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Cars)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserID)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}