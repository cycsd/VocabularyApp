using Microsoft.AspNetCore.Mvc;
using ServiceLayer.SearchService;
using ServiceLayer.SearchService.Concrete;
using System.Diagnostics;
using Vocabulary.Models;

namespace Vocabulary.Controllers
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
            var contServ = new ContentService();
            var title = contServ.GetRss();
            return View(new ContentDto { Name = title});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}