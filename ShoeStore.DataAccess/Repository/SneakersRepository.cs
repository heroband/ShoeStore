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

        public async Task<Sneakers?> GetByIdWithDetailsAsync(string id)
        {
            return await _context.Sneakers
                .Include(s => s.Comments)
                .ThenInclude(c => c.User)
                .Include(s => s.Ratings)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<SneakersShortInfoViewModel>> GetFilteredShortInfoAsync(List<string> brands, List<string> sizes)
        {
            var sneakers = await _context.Sneakers.ToListAsync();
            var filteredSneakers = sneakers.AsQueryable();

            // Фильтрация по брендам (если бренды указаны)
            if (brands != null && brands.Any())
            {
                filteredSneakers = filteredSneakers.Where(s =>
                    brands.Any(brand => s.Name.Contains(brand, StringComparison.OrdinalIgnoreCase))
                );
            }

            // Фильтрация по размерам (если размеры указаны)
            if (sizes != null && sizes.Any())
            {
                filteredSneakers = filteredSneakers.Where(s =>
                    sizes.Any(size => s.Sizes.Contains(size))
                );
            }

            return filteredSneakers.Select(s => new SneakersShortInfoViewModel
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
            sneakers.Sizes = sneakersDto.Sizes;
            sneakers.Price = sneakersDto.Price;
            sneakers.ImageUrl = sneakersDto.ImageUrl;

            await _context.SaveChangesAsync();
            return sneakers;
        }

        public async Task AddCommentAsync(string sneakersId, string userId, string content)
        {
            var comment = new Comment
            {
                SneakersId = sneakersId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task AddRatingAsync(string sneakersId, string userId, int score)
        {
            var existingRating = await _context.Ratings.FirstOrDefaultAsync(r => r.SneakersId == sneakersId && r.UserId == userId);

            if (existingRating == null)
            {
                var rating = new Rating
                {
                    SneakersId = sneakersId,
                    UserId = userId,
                    Score = score
                };
                await _context.Ratings.AddAsync(rating);
            }
            else
            {
                existingRating.Score = score;
            }

            await _context.SaveChangesAsync();
        }

    }
}
