using ASP_CORE_MVC.Data;
using ASP_CORE_MVC.Interfaces;
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
            if (ModelState.IsValid)
            {
                var sneakers = new Sneakers
                {
                    Name = sneakersDto.Name,
                    Description = sneakersDto.Description,
                    Size = sneakersDto.Size,
                    Price = sneakersDto.Price,
                };
                await _sneakersRepository.CreateAsync(sneakers);
                return Redirect("/");
            }

            Console.WriteLine(ModelState.IsValid);
            return View("Index");
        }
    }
}
