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
        Task<VocabularyDto> SearchWord(string word, int? id = null);
        Task<VocabularyDto> SearchWordOnOnlineDictionary(string word);
        IQueryable<SimpleWordInfoDto> GetWordList();
        Task<Word> InserOrUpdateWord(VocabularyDto wordDto);
        Word InsertNew(VocabularyDto wordInfo);



    }
}
