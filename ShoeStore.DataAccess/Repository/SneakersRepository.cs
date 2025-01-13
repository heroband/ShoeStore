using Microsoft.EntityFrameworkCore;
using ShoeStore.Models.Entities;
using ShoeStore.Models.Interfaces;
using ShoeStore.Models.ViewModels;

namespace ShoeStore.DataAccess.Repository
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

        public async Task<IEnumerable<SneakersShortInfoViewModel>> GetAllShortInfoAsync()
        {
            return await _context.Sneakers.Select(s => new SneakersShortInfoViewModel
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

        public async Task<IEnumerable<SneakersShortInfoViewModel>> GetFilteredShortInfoAsync(List<string> brands)
        {
            var sneakers = await _context.Sneakers.ToListAsync();

            return sneakers
                .Where(s => brands.Any(brand => s.Name.Contains(brand, StringComparison.OrdinalIgnoreCase)))
                .Select(s => new SneakersShortInfoViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    ImageUrl = s.ImageUrl,
                });
        }

        public async Task<Sneakers?> UpdateAsync(string id, SneakersViewModel sneakersDto)
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
            sneakers.ImageUrl = sneakersDto.ImageUrl;

            await _context.SaveChangesAsync();
            return sneakers;
        }
    }
}
