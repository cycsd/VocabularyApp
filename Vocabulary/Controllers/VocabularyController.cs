using DataLayer.EFCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using ServiceLayer.DictionaryService;
using ServiceLayer.DictionaryService.Concrete;
using System.ComponentModel.Design;
using System.Net.Http.Headers;

namespace Vocabulary.Controllers
{
    public class VocabularyController : Controller
    {
        private readonly ICoreDictionaryService _dictionaryService;

        public VocabularyController(ICoreDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public IActionResult Index(SortFilterOptions options)
        {
            var wordList = _dictionaryService.GetWordListWithFilter(options).ToList();
            return View(new WordListCombineFilterDto (options,wordList));
        }

        public async Task<IActionResult> WordDetail(int id, string word)
        {

            var w = await _dictionaryService.SearchWord(word, id);
            return View(w);
        }

        public async Task<IActionResult> SimpleSave(VocabularyDto wordDto)
        {
            try
            {
                if (wordDto.wordId == null && !string.IsNullOrWhiteSpace(wordDto.word))
                {
                    wordDto = (await _dictionaryService.SearchWordOnOnlineDictionary(wordDto.word)).First();
                    var resp = _dictionaryService.InsertNew(wordDto);
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
                var resp = await _dictionaryService.InserOrUpdateWord(wordDto);
                return Ok(new VocabularyDto { word = resp.Text, wordId = resp.WordId });
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
                var word = await _dictionaryService.SearchWord(keywords);
                return Ok(word);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
