using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.SearchService;
using ServiceLayer.SearchService.Concrete;

namespace Vocabulary.Controllers
{
    public class ArticleController : Controller
    {
        public async Task<ActionResult> Index(ParagraphDto paragraph)
        {
            var serv = new ArticleService();
            var html = await serv.ParseParagraph(paragraph);
            return View(html);
        }

        public IActionResult Read(string articleUri)
        {
            return RedirectToAction("Index", new ParagraphDto { Uri = articleUri });
        }
    }
}
