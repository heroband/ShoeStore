using System.Diagnostics;
using ShoeStore.Models.Interfaces;
using ShoeStore.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ShoeStore.Controllers
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost("AllProducts/Edit/{id}")]
        public async Task<IActionResult> Edit(string id, SneakersViewModel sneakersDto)
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

        [Authorize(Roles = "Admin")]
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
