using AngleSharp;
using AngleSharp.Html.Dom;
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


        public async Task<ParagraphDto> GetAndParseParagraph(ParagraphDto paragraph)
        {

            var config = Configuration.Default.WithDefaultLoader().WithCss();
            var context = BrowsingContext.New(config);
            var doc = await context.OpenAsync(paragraph.Uri);
            var content = doc.QuerySelector("div.wsw");
            var mediaHolder = content?.QuerySelector("div.wsw__embed");
            var audio = (mediaHolder?.QuerySelector("a.c-mmp__fallback-link") as IHtmlAnchorElement)?.Href;
            content?.RemoveChild(mediaHolder);

            return new ParagraphDto { 
                Uri = paragraph.Uri,
                AudioUri=audio, 
                Content = content.InnerHtml };
        }
    }


}
