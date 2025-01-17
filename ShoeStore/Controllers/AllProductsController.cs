using System.Diagnostics;
using ShoeStore.Models.Interfaces;
using ShoeStore.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ShoeStore.Models.Entities;
using System.Security.Claims;

namespace ShoeStore.Controllers
{
    public class AllProductsController : Controller
    {
        private readonly ISneakersRepository _sneakersRepository;
        public AllProductsController(ISneakersRepository sneakersRepository)
        {
            _sneakersRepository = sneakersRepository;
        }
        public async Task<IActionResult> Index(FilterViewModel filter)
        {
            if ((filter.SelectedBrands == null || filter.SelectedBrands.Count == 0) &&
                (filter.SelectedSizes == null || filter.SelectedSizes.Count == 0))
            {
                filter.Sneakers = await _sneakersRepository.GetAllShortInfoAsync();
            }
            else
            {
                filter.Sneakers = await _sneakersRepository.GetFilteredShortInfoAsync(filter.SelectedBrands, filter.SelectedSizes);
            }
            return View(filter);
        }

        [HttpGet("AllProducts/Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var sneakers = await _sneakersRepository.GetByIdWithDetailsAsync(id);
            if (sneakers == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }

            var viewModel = new SneakersDetailsViewModel
            {
                Sneakers = sneakers,
                Comments = sneakers.Comments.OrderByDescending(c => c.CreatedAt).ToList(),
                AverageRating = sneakers.Ratings.Any() ? sneakers.Ratings.Average(r => r.Score) : 0
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost("AllProducts/Details/{id}/AddComment")]
        public async Task<IActionResult> AddComment(string id, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Comment cannot be empty.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _sneakersRepository.AddCommentAsync(id, userId, content);

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost("AllProducts/Details/{id}/AddRating")]
        public async Task<IActionResult> AddRating(string id, int score)
        {
            if (score < 1 || score > 5)
            {
                TempData["Error"] = "Rating must be between 1 and 5.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _sneakersRepository.AddRatingAsync(id, userId, score);

            return RedirectToAction(nameof(Details), new { id });
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
