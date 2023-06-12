using DataLayer.EFCode;
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

        public async Task<IActionResult> Word() {

            var word =await new DictionaryService().SearchWord("glance");
            return View(word);
        }


        public IActionResult Save(VocabularyDto word)
        {

            return Ok();
        }
        public async Task<IActionResult> Search(string keywords, [FromServices]VocabularyAppContext context)
        {
            var dic = new DictionaryService();
            var word = await dic.SearchWord(keywords);
            dic.AddWord(word, context);
            return Ok(word);
        }
    }
}
