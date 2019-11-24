using Microsoft.EntityFrameworkCore;
using alintaApi.Models;

namespace alintaApi.Data
{
    public class CustomersDbContext : DbContext
    { 
        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
