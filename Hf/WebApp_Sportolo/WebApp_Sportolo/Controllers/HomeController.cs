using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp_Sportolo.Models;

namespace WebApp_Sportolo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult sportolo() 
        {
            ViewData["Name"] = "Sanyi";
            ViewData["BirthDate"] = new DateTime(1995, 5, 15);
            ViewData["Events"] = new List<string> { "100m Sprint", "200m Sprint", "4x100m Relay" };
            ViewData["PhotoUrl"] = "https://testszerviz.hu/evcms_medias/upload/images/kimbinoegy.jpg";
            return View();

        }
       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
