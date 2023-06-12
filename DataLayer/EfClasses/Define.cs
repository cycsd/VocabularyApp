using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EfClasses
{
    public  class Define
    {
        public int DefineId { get; set; }
        public string Definition { get; set; }

        public string? Example { get; set; }

        public int VocabularyId { get; set; }

        public  Vocabulary Vocabulary { get; set; }
    }
}
