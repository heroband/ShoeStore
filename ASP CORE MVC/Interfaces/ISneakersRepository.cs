using ASP_CORE_MVC.Models;
using ASP_CORE_MVC.Models.Dto;

namespace ASP_CORE_MVC.Interfaces
{
    public interface ISneakersRepository
    {
        Task<Sneakers> CreateAsync(Sneakers sneakersModel);
        Task<IEnumerable<SneakersShortInfoDto>> GetAllShortInfoAsync();
        Task<Sneakers?> GetByIdAsync(string id);
        Task<Sneakers?> UpdateAsync(string id, SneakersDto sneakersDto);
        Task<Sneakers?> DeleteAsync(string id);
    }
}
