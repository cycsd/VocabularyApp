using Microsoft.AspNetCore.Mvc;

namespace Vocabulary.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
