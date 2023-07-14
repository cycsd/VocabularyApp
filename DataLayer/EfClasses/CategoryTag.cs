using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EfClasses
{
    public class CategoryTag
    {
        public int CategoryTagId { get; set; }
        public string Name { get; set; }

        public ICollection<Word> Words { get; set; }


    }
}
