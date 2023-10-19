using doanhuuthanh_web.backendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace doanhuuthanh_web.backendApi.Controllers
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

      
    }
}