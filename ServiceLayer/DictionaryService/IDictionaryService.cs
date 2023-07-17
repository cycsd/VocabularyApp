using DataLayer.EfClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService
{
    public interface ICoreDictionaryService
    {
        Task<IQueryable<Word>> SearchWord(string word, int? id = null);
        Task<IEnumerable<VocabularyDto>> SearchWordOnOnlineDictionary(string word);
        IQueryable<SimpleWordInfoDto> GetWordListWithFilter(SortFilterOptions options);
        Task<Word> InserOrUpdateWord(VocabularyDto wordDto);
        Word InsertNew(VocabularyDto wordInfo);
        Task<Word> InsertNew(SaveWordDto wordInfo);
        Word ChangeCategories(SaveWordDto wordInfo);

    }
}
