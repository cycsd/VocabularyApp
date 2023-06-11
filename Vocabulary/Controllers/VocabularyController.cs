using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.DictionaryService;
using ServiceLayer.DictionaryService.Concrete;
using System.Net.Http.Headers;

namespace Vocabulary.Controllers
{
    public class VocabularyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Word() {

            var word = new DictionaryService().SearchWord("glance");
            return View(word);
        }


        public IActionResult Save(VocabularyDto word)
        {

            return Ok();
        }
        public JsonResult Search(string keywords)
        {
            var dic = new DictionaryService();
            var word = dic.SearchWord(keywords);
            return Json(word);
        }
    }
}
