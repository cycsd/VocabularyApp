using Microsoft.EntityFrameworkCore;

namespace DataLayer.EfClasses
{
    public class Word
    {
        public int WordId { get; set; }

        public string Text { get; set; }

        public ICollection<Vocabulary> Vocabularies { get; set; }
    }
}