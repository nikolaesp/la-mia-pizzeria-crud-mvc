using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeria.Models
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizza { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=DBPizza;" +
            "Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
