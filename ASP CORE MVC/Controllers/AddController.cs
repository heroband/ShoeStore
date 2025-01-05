using ASP_CORE_MVC.Data;
using ASP_CORE_MVC.Interfaces;
using ASP_CORE_MVC.Mappers;
using ASP_CORE_MVC.Models;
using ASP_CORE_MVC.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_MVC.Controllers
{
    public class AddController : Controller
    {
        private readonly ISneakersRepository _sneakersRepository;
        public AddController(ISneakersRepository sneakersRepository)
        {
            _sneakersRepository = sneakersRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Check(SneakersDto sneakersDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var sneakers = sneakersDto.ToSneakersFromDto();
            await _sneakersRepository.CreateAsync(sneakers);
            return Redirect("/AllProducts");
        }
    }
}
