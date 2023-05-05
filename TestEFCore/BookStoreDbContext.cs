using EsempioADO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEFCore
{
    public class BookStoreDbContext : DbContext
    {
        public DbSet<Book>   Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30";

            optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder.LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description)
                    .HasMaxLength(512);

                entity.Property(e => e.Genre)
                    .HasMaxLength(256)
                    .HasConversion<string>();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(256);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId); ;
            });
        }
    }
}
