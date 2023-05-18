using Microsoft.EntityFrameworkCore;
using TestWebApi.Entities;

namespace TestWebApi.Infrastructure;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);
    }
}
