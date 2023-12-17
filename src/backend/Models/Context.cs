using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class Context : DbContext {

    public DbSet<User> users { get; set; }

    public string DbPath { get; }
    public Context() {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "database.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}