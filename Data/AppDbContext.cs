using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StoreApi2.User;

namespace StoreApi2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User1> User1s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseNpgsql("Server=localhost;" +
                                     "Port=5432;" +
                                     "Database=User1;" +
                                     "User Id=postgres;" +
                                     "Password=123;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
