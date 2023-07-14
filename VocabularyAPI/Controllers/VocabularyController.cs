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
            var categories = _tagService.GetCategoryTags();
            return Ok(categories);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> WordDetail(int? id, string word)
        {

            var vocabulary = await _dictionaryService.SearchWord(word, id);
            var simpleWordInfo = vocabulary
                                    .ProjectToWord()
                                    .MapWordToSimpleWordDto();
            return Ok(simpleWordInfo);
        }
    }
}
