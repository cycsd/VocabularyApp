using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService
{
    public class WordListCombineFilterDto
    {
        public WordListCombineFilterDto(SortFilterOptions sortFilterData, IEnumerable<SimpleWordInfoDto> wordList)
        {
            SortFilterData = sortFilterData;
            WordList = wordList;
        }

        public SortFilterOptions SortFilterData { get; set; }
        public IEnumerable<SimpleWordInfoDto> WordList { get; set; }
    }
    
}
