using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EfClasses
{
    public class Vocabulary
    {
        public int VocabularyId { get; set; }
        public Word Word { get; set; }

        public ICollection<Define> Definitions { get; set; }
    }
}
