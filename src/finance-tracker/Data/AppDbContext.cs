using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(Environment.CurrentDirectory, "Data", "app.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}
