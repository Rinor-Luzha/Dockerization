using Microsoft.EntityFrameworkCore;
using Dockerization.Models.Entities;

namespace Dockerization.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Pet> Pets { get; set; }

    }
}
