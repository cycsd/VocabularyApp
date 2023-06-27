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
        [HttpGet()]
        public IEnumerable<ArticleDto> GetArticle()
        {
            var serv = new ArticleService();
            var articeList = serv.GetArticles();
            return articeList;
        }

    }
}
