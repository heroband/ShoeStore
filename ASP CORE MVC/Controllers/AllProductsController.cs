using System.Diagnostics;
using ASP_CORE_MVC.Interfaces;
using ASP_CORE_MVC.Mappers;
using ASP_CORE_MVC.Models;
using ASP_CORE_MVC.Models.Dto;
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
            var sneakers = await _sneakersRepository.GetAllShortInfoAsync();
            return View(sneakers);
        }

        [HttpGet("AllProducts/Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var sneakers = await _sneakersRepository.GetByIdAsync(id);
            if (sneakers == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
            return View(sneakers);
        }

        [HttpGet("AllProducts/Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var sneakers = await _sneakersRepository.GetByIdAsync(id);
            if (sneakers == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }

            var sneakersDto = sneakers.ToSneakersDto();
            return View(sneakersDto);
        }

        [HttpPost("AllProducts/Edit/{id}")]
        public async Task<IActionResult> Edit(string id, SneakersDto sneakersDto)
        {
            if (!ModelState.IsValid)
            {
                return View(sneakersDto);
            }

            var sneakers = await _sneakersRepository.UpdateAsync(id, sneakersDto);
            if (sneakers == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }

            return Redirect("/AllProducts");
        }

        [HttpPost("AllProducts/Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var sneakers = await _sneakersRepository.DeleteAsync(id);

            if (sneakers == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }

            return Redirect("/AllProducts");
        }
    }
}
