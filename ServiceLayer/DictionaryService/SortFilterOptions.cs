using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService
{
    public class SortFilterOptions
    {
        public string? Text { get; set; }
        public IEnumerable<KeyValuePair>? Categories { get; set; }
    }
}
