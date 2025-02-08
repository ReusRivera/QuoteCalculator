using Microsoft.AspNetCore.Mvc;

namespace QuoteCalculator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
