using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService
{
    public class SaveWordDto
    {
        public int? WordId { get; set; }
        public string Text { get; set; }
        public IEnumerable<KeyValuePair> Categories { get; set; }
    }
}
