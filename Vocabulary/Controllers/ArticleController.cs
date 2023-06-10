using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.SearchService;
using ServiceLayer.SearchService.Concrete;

namespace Vocabulary.Controllers
{
    public class ArticleController : Controller
    {
        // GET: ArticleController
        public ActionResult Index(ParagraphDto paragraph)
        {
            var serv = new ArticleService();
            var html = serv.ParseParagraph(paragraph);
            return View(html);
        }

        public IActionResult Read(string articleUri)
        {
            return RedirectToAction("Index", new ParagraphDto { Uri = articleUri });
        }

        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArticleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArticleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
