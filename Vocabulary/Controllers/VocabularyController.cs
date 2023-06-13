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
        private readonly VocabularyAppContext _context;
        public VocabularyController(VocabularyAppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var wordList = new DictionaryService(_context).WordList().ToList();
            return View(wordList);
        }

        public async Task<IActionResult> WordDetail(int id, string word)
        {

            var w = await new DictionaryService(_context).SearchWord(word, id);
            return View(w);
        }


        public IActionResult Save(VocabularyDto wordDto)
        {
            var service = new DictionaryService(_context);
            service.InserOrUpdateWord(wordDto);
            return Ok();
        }
        public async Task<IActionResult> Search(string keywords)
        {
            try
            {
                var dic = new DictionaryService(_context);
                var word = await dic.SearchWord(keywords);

                return Ok(word);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
