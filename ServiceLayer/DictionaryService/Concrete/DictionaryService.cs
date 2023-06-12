using DataLayer.EfClasses;
using DataLayer.EFCode;
using Newtonsoft.Json;
using ServiceLayer.SearchService.Mapping;
using System.Net.Http.Headers;

namespace ServiceLayer.DictionaryService.Concrete
{
    public class DictionaryService
    {

        public async Task<VocabularyDto> SearchWord(string word)
        {
            var apiUrl = @$"https://api.dictionaryapi.dev/api/v2/entries/en/{word.Trim()}";
            var client = new HttpClient ();
            var response = await client.GetAsync(apiUrl);  
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var voc =await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<VocabularyDto>>(voc).First();
            }
            else
            {
                return  await Task.FromException<VocabularyDto>(new KeyNotFoundException());
            }

        }
        public Word AddWord(VocabularyDto wordDto,VocabularyAppContext context)
        {
            try {
                var word = wordDto.ProjectToWord();
                context.Words.Add(word);
                context.SaveChanges();
                return word;
            } catch (Exception ex) 
            {
                throw ex;

            }

        }

    }
}
