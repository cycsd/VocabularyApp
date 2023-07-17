using DataLayer.EfClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService.Query
{
    public static class TagDtoMapping
    {
        public static IEnumerable<KeyValuePair> MapToKeyValuePair(this IEnumerable<CategoryTag> source)
            => source.Select(tag => tag.MapToKeyValuePair());

        public static KeyValuePair MapToKeyValuePair(this CategoryTag categoryTag)
         => new KeyValuePair
         {
             Key = categoryTag.CategoryTagId.ToString(),
             Value = categoryTag.Name,
         };


    }
}
