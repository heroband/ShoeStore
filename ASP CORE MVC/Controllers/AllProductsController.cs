using ASP_CORE_MVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_MVC.Controllers
{
    public class AllProductsController : Controller
    {
        private readonly ISneakersRepository _sneakersRepository;
        public AllProductsController(ISneakersRepository sneakersRepository)
        {
            _sneakersRepository = sneakersRepository;
        }
        public async Task<IActionResult> Index()
        {
            var sneakers = await _sneakersRepository.GetAllAsync();
            return View(sneakers);
        }
    }
}
