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
            if (!string.IsNullOrWhiteSpace(options.Text))
                words = words.Where(w => w.Text.Contains(options.Text));
            if (options.Categories != null)
            {
                var id = options.Categories.First().Key;
                words = words.ToList().Where(w => w.Categories.Any(c => c.Key == id)).AsQueryable();
            }
            return words;
        }
    }
}
