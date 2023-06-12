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
    public static class Mapping
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

        public static Word ProjectToWord(this VocabularyDto wordDto)
        {
            var word = new Word
            {
                Text = wordDto.word,
                Vocabularies = wordDto.meanings
                .Select(mean => new Vocabulary
                {
                    PartOfSpeech = mean.partOfSpeech,
                    IPA = wordDto.phonetics.FirstOrDefault(ph=>!string.IsNullOrWhiteSpace(ph.text))?.text,
                    Pronounce = wordDto.phonetics.FirstOrDefault(ph=> !string.IsNullOrWhiteSpace(ph.audio))?.audio,
                    Definitions = mean.definitions
                                        .Select(def =>
                                        new Define
                                        {
                                            Definition = def.definition,
                                            Example = def.example
                                        }).ToList()
                }).ToList(),

            };
            return word;
        }


    }
}
