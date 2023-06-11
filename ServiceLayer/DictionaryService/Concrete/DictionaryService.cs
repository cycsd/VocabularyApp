using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ServiceLayer.DictionaryService.Concrete
{
    public class DictionaryService
    {

        public VocabularyDto SearchWord(string word)
        {
            var apiUrl = @$"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";
            var client = new HttpClient ();
            var response = client.GetAsync(apiUrl).Result;  
            var voc = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<VocabularyDto>>(voc).First();
        }
    }
}
