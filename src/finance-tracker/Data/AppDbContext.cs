using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TreeNode<string>> CategoryTrees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(Environment.CurrentDirectory, "Data", "app.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TreeNode<string>>()
            .HasKey(t => t.Id); // Define a key for TreeNode<string>

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(u => u.CategoryTree)
                .WithOne()
                .HasForeignKey<User>(u => u.CategoryTreeId);
        });

        base.OnModelCreating(modelBuilder);
}

}
