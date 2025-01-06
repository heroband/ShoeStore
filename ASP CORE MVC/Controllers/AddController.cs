using ShoeStore.Models.Interfaces;
using ShoeStore.Models.Mappers;
using ShoeStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ShoeStore.Controllers
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
        public async Task<IActionResult> Check(SneakersViewModel sneakersDto)
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
