using ASP_CORE_MVC.Data;
using ASP_CORE_MVC.Interfaces;
using ASP_CORE_MVC.Models;
using ASP_CORE_MVC.Models.Dto;
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

        public async Task<Sneakers?> DeleteAsync(string id)
        {
            var sneakers = await _context.Sneakers.FirstOrDefaultAsync(s => s.Id == id);
            if (sneakers == null)
            {
                return null;
            }

            _context.Sneakers.Remove(sneakers);
            await _context.SaveChangesAsync();

            return sneakers;
        }

        public async Task<IEnumerable<SneakersShortInfoDto>> GetAllShortInfoAsync()
        {
            return await _context.Sneakers.Select(s => new SneakersShortInfoDto
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                ImageUrl = s.ImageUrl,
            }).ToListAsync();
        }

        public async Task<Sneakers?> GetByIdAsync(string id)
        {
            return await _context.Sneakers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sneakers?> UpdateAsync(string id, SneakersDto sneakersDto)
        {
            var sneakers = await _context.Sneakers.FirstOrDefaultAsync(s => s.Id == id);

            if (sneakers == null)
            {
                return null;
            }

            sneakers.Name = sneakersDto.Name;
            sneakers.Description = sneakersDto.Description;
            sneakers.Size = sneakersDto.Size;
            sneakers.Price = sneakersDto.Price;

            await _context.SaveChangesAsync();
            return sneakers;
        }
    }
}
