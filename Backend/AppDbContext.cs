using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using JwtTest.Models;

namespace JwtTest.PostgreSQL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234");
    }
}
