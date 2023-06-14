using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService.Query
{
    public static class DictionaryFilter
    {
        public static IQueryable<SimpleWordInfoDto> FilterWordInfoBy(this IQueryable<SimpleWordInfoDto>words, SortFilterOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.SearchText))
                words = words.Where(w => w.Text.Contains(options.SearchText));
            return words;
        }
    }
}
