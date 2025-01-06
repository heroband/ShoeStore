using Microsoft.EntityFrameworkCore;
using ShoeStore.Models.Entities;

namespace ShoeStore.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Sneakers> Sneakers { get; set; }
    }
}
