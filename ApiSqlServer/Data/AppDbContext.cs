using ApiSqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSqlServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Productos { get; set; }
        public DbSet<Availability> Disponibilidad { get; set; }

     

    }
}
