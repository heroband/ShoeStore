using ShoeStore.Models.Entities;
using ShoeStore.Models.ViewModels;

namespace ShoeStore.Models.Interfaces
{
    public interface ISneakersRepository
    {
        Task<Sneakers> CreateAsync(Sneakers sneakersModel);
        Task<IEnumerable<SneakersShortInfoViewModel>> GetAllShortInfoAsync();
        Task<IEnumerable<SneakersShortInfoViewModel>> GetFilteredShortInfoAsync(List<string> brands);
        Task<Sneakers?> GetByIdAsync(string id);
        Task<Sneakers?> UpdateAsync(string id, SneakersViewModel sneakersDto);
        Task<Sneakers?> DeleteAsync(string id);
    }
}
