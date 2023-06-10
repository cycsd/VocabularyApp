using AngleSharp;
using HtmlAgilityPack;
using Microsoft.VisualBasic;
using ServiceLayer.SearchService.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ServiceLayer.SearchService.Concrete
{
    public class ArticleService
    {

        public IEnumerable<ArticleDto> GetArticles()
        {
            var url = "https://learningenglish.voanews.com/api/zpyp_e-rm_";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            
            return feed.MapVoaToArticleDto();
        }


        public ParagraphDto ParseParagraph(ParagraphDto paragraph)
        {

            var config = Configuration.Default.WithDefaultLoader().WithCss();
            var context = BrowsingContext.New(config);
            var doc = context.OpenAsync(paragraph.Uri).Result;
            var content = doc.QuerySelector("div.wsw");

            return new ParagraphDto { Uri = paragraph.Uri, Content = content.InnerHtml };
        }
    }


}
