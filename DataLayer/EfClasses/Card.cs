using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EfClasses
{
    public class Card
    {
        public int CardId { get; set; }
        public Word Word { get; set; }

        public DateTime ReviewTime { get; set; }

    }
}
