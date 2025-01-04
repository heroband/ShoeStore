using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_MVC.Controllers
{
    public class AllProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
