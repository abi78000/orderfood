
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();
    public DbSet<BillDetail> BillDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BillDetail>().ToTable("BillDetails", "public");
    }
}
