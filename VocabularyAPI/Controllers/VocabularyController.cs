using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DictionaryService;
using ServiceLayer.DictionaryService.Query;

namespace VocabularyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabularyController : ControllerBase
    {
        private readonly ICoreDictionaryService _dictionaryService;
        public VocabularyController(ICoreDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [HttpGet()]
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
