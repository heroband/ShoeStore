using ASP_CORE_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_MVC.Controllers
{
    public class AddController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check(Sneakers sneakers)
        {
            if (ModelState.IsValid)
            {
                return Redirect("/");
            }

            return View("Index");
        }
    }
}
