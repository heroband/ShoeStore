using ASP_CORE_MVC.Data;
using ASP_CORE_MVC.Interfaces;
using ASP_CORE_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_CORE_MVC.Repository
{
    public class SneakersRepository : ISneakersRepository
    {
        private ApplicationDbContext _context;
        public SneakersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Sneakers> CreateAsync(Sneakers sneakersModel)
        {
            await _context.Sneakers.AddAsync(sneakersModel);
            await _context.SaveChangesAsync();
            return sneakersModel;
        }

        public async Task<IEnumerable<Sneakers>> GetAllAsync()
        {
            return await _context.Sneakers.ToListAsync();
        }
    }
}
