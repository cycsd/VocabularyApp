using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DataLayer.EfClasses;
using ServiceLayer.DictionaryService;

namespace ServiceLayer.SearchService.Mapping
{
    public static class ServiceDtoMapping
    {
        public static IEnumerable<ArticleDto> MapVoaToArticleDto(this SyndicationFeed feed)
        {
            var notMp3 = (SyndicationItem item) => !item.Title.Text.Contains(".mp3");
            var isArticle = (SyndicationItem item) => item.Categories.Count > 1;
            return feed.MapVoaToArticleDtoWithFilter(filter: notMp3 + isArticle);
        }
        public static IEnumerable<ArticleDto> MapVoaToArticleDtoWithFilter(
            this SyndicationFeed feed,
            Func<SyndicationItem, bool> filter)
        => feed.Items.Where(filter).Select(item => new ArticleDto
        {
            Title = item.Title.Text,
            Uri = item.Links[0].Uri,
            ImageUri = item.Links[1].Uri,
            PublishDate = item.PublishDate.ToString("yyyy/MM/dd"),

        });

 


    }
}
