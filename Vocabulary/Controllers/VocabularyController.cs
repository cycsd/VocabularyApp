using DataLayer.EFCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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

        public async Task<IActionResult> SimpleSave(VocabularyDto wordDto)
        {
            try
            {
                if (wordDto.wordId == null && !string.IsNullOrWhiteSpace(wordDto.word))
                {
                    var service = new DictionaryService(_context);
                    wordDto = await service.SearchWordByApi(wordDto.word);
                    var resp = service.InsertNew(wordDto);
                    return Ok(new VocabularyDto { word = resp.Text, wordId = resp.WordId });
                }
                return Ok(wordDto);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> Save(VocabularyDto wordDto)
        {
            try
            {
                var service = new DictionaryService(_context);
                var resp = await service.InserOrUpdateWord(wordDto);
                return Ok(new VocabularyDto { word=resp.Text,wordId=resp.WordId});
            }
            catch (Exception ex)
            {
                return NotFound();
            }
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
