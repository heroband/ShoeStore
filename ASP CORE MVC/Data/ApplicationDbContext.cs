using ASP_CORE_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_CORE_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Sneakers> Sneakers { get; set; }
    }
}
