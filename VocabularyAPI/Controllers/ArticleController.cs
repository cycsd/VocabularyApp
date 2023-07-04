using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.SearchService;
using ServiceLayer.SearchService.Concrete;

namespace VocabularyAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ArticleController : ControllerBase
    {
        [HttpGet("articles")]
        public IActionResult GetArticles()
        {
            var serv = new ArticleService();
            var articeList = serv.GetArticles();
            return Ok(articeList);
        }
        [HttpGet("paragraph")]
        public async Task<IActionResult> GetParagraph(string path)
        {
            var serv = new ArticleService();
            var paragraph =await serv.GetAndParseParagraph(new ParagraphDto { Uri=path});
            return Ok(paragraph);
        }

    }
}
