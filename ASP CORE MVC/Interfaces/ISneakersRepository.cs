using ASP_CORE_MVC.Models;

namespace ASP_CORE_MVC.Interfaces
{
    public interface ISneakersRepository
    {
        Task<Sneakers> CreateAsync(Sneakers sneakersModel);
    }
}
