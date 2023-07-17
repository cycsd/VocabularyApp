using DataLayer.EfClasses;
using DataLayer.EFCode;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceLayer.DictionaryService.Query;
using ServiceLayer.HelperExtension;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace ServiceLayer.DictionaryService.Concrete
{
    public class DictionaryService : ICoreDictionaryService
    {
        private readonly VocabularyAppContext _context;
        public DictionaryService(VocabularyAppContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Word>> SearchWord(string word, int? id = null)
        {
            Expression<Func<Word, bool>> dbSearchCondition =
                id == null
                   ? w => w.Text == word
                   : w => w.WordId == id;

            var SearchStrategies = new Func<Task<IQueryable<Word>>>[]
            {
                ()=>SearchWordInDb(dbSearchCondition),
                ()=>SearchWordOnOnlineDictionary(word)
                    .ContinueWith(w=>w.Result.Select(v=>v.MapToWord())
                                            .AsQueryable(),TaskContinuationOptions.OnlyOnRanToCompletion)                    ,
            };
            return await SearchStrategies.Aggregate((search, fallback) =>
                                       () => search().OrElse(fallback))();

        }
        private Task<IQueryable<Word>> SearchWordInDb(
            Expression<Func<Word, bool>> predict
            )
        {
            var word = _context.Words
                        .Where(predict);


            return word.FirstOrDefault() != null
                        ? Task.FromResult(word)
                        : Task.FromException<IQueryable<Word>>(new KeyNotFoundException());
        }

        public async Task<IEnumerable<VocabularyDto>> SearchWordOnOnlineDictionary(string word)
        {
            var apiUrl = @$"https://api.dictionaryapi.dev/api/v2/entries/en/{word?.Trim()}";
            var client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var voc = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<VocabularyDto>>(voc);
            }
            else
            {
                return await Task.FromException<IEnumerable<VocabularyDto>>(new KeyNotFoundException());
            }
        }

        public IQueryable<SimpleWordInfoDto> GetWordListWithFilter(SortFilterOptions options)
        {
            var wordInfo = _context.Words
                            .AsNoTracking()
                            .MapWordToSimpleWordDto()
                            .FilterWordInfoBy(options);
            return wordInfo;
        }


        public async Task<Word> InserOrUpdateWord(VocabularyDto wordDto)
        {
            try
            {
                if (wordDto.wordId == null)
                {
                    var word = await SearchWordOnOnlineDictionary(wordDto.word);
                    return InsertNew(word.First());
                }
                else
                {
                    return UpdateWord(new Word { WordId = (int)wordDto.wordId, Note = wordDto.note });
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public Word InsertNew(VocabularyDto wordInfo)
        {
            var word = wordInfo.MapToWord();
            _context.Words.Add(word);
            _context.SaveChanges();
            return word;
        }
        public async Task<Word> InsertNew(SaveWordDto wordInfo)
        {
            var voc = await SearchWordOnOnlineDictionary(wordInfo.Text);
            var categoriedId = wordInfo.Categories.Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key))
                              .Select(kvp => int.Parse(kvp.Key)).ToList();

            var word = voc.First().MapToWord();
            word.CategoryTags = _context.CategoryTags
                                .Where(c => categoriedId.Contains(c.CategoryTagId))
                                .ToList();
            _context.Words.Add(word);
            _context.SaveChanges();
            return word;
        }
        private Word UpdateWord(Word wordFromDto)
        {
            var word = new Word { WordId = wordFromDto.WordId };
            _context.Entry(word).State = EntityState.Unchanged;
            word.Note = wordFromDto.Note;

            _context.SaveChanges();
            return word;
        }

        public Word ChangeCategories(SaveWordDto wordInfo)
        {
            if (wordInfo.WordId == null) throw new ArgumentNullException();
            var categoriedId = wordInfo.Categories.Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key))
                  .Select(kvp => int.Parse(kvp.Key)).ToList();
            var word = _context.Words.Include(w=>w.CategoryTags).First(w => w.WordId == wordInfo.WordId);
            _context.Entry(word).State = EntityState.Unchanged;
            word.CategoryTags.Clear();
            word.CategoryTags = _context.CategoryTags
                                .Where(c => categoriedId.Contains(c.CategoryTagId))
                                .ToList();
            _context.SaveChanges();
            return word;
        }
    }
}
