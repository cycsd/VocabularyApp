using DataLayer.EfClasses;
using DataLayer.EFCode;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceLayer.DictionaryService.Mapping;
using ServiceLayer.HelperExtension;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace ServiceLayer.DictionaryService.Concrete
{
    public class DictionaryService
    {
        private readonly VocabularyAppContext _context;
        public DictionaryService(VocabularyAppContext context)
        {
            _context = context;
        }

        public async Task<VocabularyDto> SearchWord(string word, int? id = null)
        {
            Expression<Func<Word, bool>> dbSearchCondition = 
                id == null
                   ? w => w.Text == word
                   : w => w.WordId == id;

            var SearchStrategies = new Func<Task<VocabularyDto>>[]
            {
                ()=>SearchWordInDb(dbSearchCondition),
                ()=>SearchWordByApi(word),
            };
            return await SearchStrategies.Aggregate((search, fallback) =>
                                       () => search().OrElse(fallback))();

        }
        private Task<VocabularyDto> SearchWordInDb(
            Expression<Func<Word, bool>> predict
            )
        {
            var word = _context.Words
               .Where(predict)
               .Select(w => new VocabularyDto
               {
                   wordId = w.WordId,
                   word = w.Text,
                   note = w.Note,
                   phonetics = w.Vocabularies.Select(vocabulary =>
                                new Phonetic
                                {
                                    text = vocabulary.IPA,
                                    audio = vocabulary.Pronounce,
                                }).ToList(),
                   meanings = (from vocabulary in w.Vocabularies
                               select new Meaning
                               {
                                   partOfSpeech = vocabulary.PartOfSpeech,
                                   definitions = (from define in vocabulary.Definitions
                                                  select new Definition
                                                  {
                                                      definition = define.Definition,
                                                      example = define.Example,
                                                  }).ToList(),
                               }).ToList()

               })
                 .FirstOrDefault();


            return word != null
                        ? Task.FromResult(word)
                        : Task.FromException<VocabularyDto>(new KeyNotFoundException());
        }

        public async Task<VocabularyDto> SearchWordByApi(string word)
        {
            var apiUrl = @$"https://api.dictionaryapi.dev/api/v2/entries/en/{word?.Trim()}";
            var client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var voc = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<VocabularyDto>>(voc).First();
            }
            else
            {
                return await Task.FromException<VocabularyDto>(new KeyNotFoundException());
            }
        }

        public IQueryable<SimpleWordInfoDto> WordList()
        {
            var wordInfo = _context.Words.Select(w => new SimpleWordInfoDto
            {
                WordId = w.WordId,
                Text = w.Text,
                Note = w.Note,
                PartOfSpeech = w.Vocabularies.Select(v =>
                new DefinitionInfoDto
                {
                    PartOfSpeech = v.PartOfSpeech,
                    Definitions = v.Definitions.Select(d => d.Definition)
                }),
                PronuanceAudioUrl = w.Vocabularies.Select(v => v.Pronounce)
                                        .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p))
                                        ?? string.Empty,
            });
            return wordInfo;
        }

        public async Task<Word> InserOrUpdateWord(VocabularyDto wordDto)
        {
            try
            {
                if (wordDto.wordId == null)
                {
                    var word = await SearchWordByApi(wordDto.word);
                    return InsertNew(word);
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
            var word = wordInfo.ProjectToWord();
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

    }
}
