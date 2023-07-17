using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DictionaryService;
using ServiceLayer.DictionaryService.Concrete;
using ServiceLayer.DictionaryService.Query;

namespace VocabularyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private readonly ICoreDictionaryService _dictionaryService;
        private readonly TagService _tagService;

        public VocabularyController(ICoreDictionaryService dictionaryService, TagService tagService)
        {
            _dictionaryService = dictionaryService;
            _tagService = tagService;
        }

        [HttpGet("[action]")]
        public IActionResult Categories()
        {
            var categories = _tagService.GetCategoryTags()
                                        .MapToKeyValuePair();
            return Ok(categories);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> WordDetail(int? id, string word)
        {
            try
            {
                var w = await _dictionaryService.SearchWord(word, id);
                var simpleWordInfo = w.MapWordToSimpleWordDto().First();
                return Ok(simpleWordInfo);
            }
            catch (Exception e)
            {

                throw;
            }

        }

        [HttpPost("[action]")]
        public IActionResult Words(SortFilterOptions options)
        {
            try
            {
                var wordList = _dictionaryService.GetWordListWithFilter(options).ToList();
                return Ok(wordList);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SaveNew(SaveWordDto wordInfo)
        {
            try
            {
                var saved = await _dictionaryService.InsertNew(wordInfo);
                return Ok(new { text = saved.Text, id = saved.WordId });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost("[action]")]
        public IActionResult ChangeCategories(SaveWordDto wordInfo)
        {
            try
            {
                var saved = _dictionaryService.ChangeCategories(wordInfo);
                return Ok(new { text = saved.Text, id = saved.WordId });
            }
            catch (ArgumentNullException e)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
