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

            optionsBuilder.LogTo(message=>Debug.WriteLine(message));
        }
    }
}
