using Microsoft.EntityFrameworkCore;

namespace DataLayer.EfClasses
{
    public class Word
    {
        public int WordId { get; set; }

        public string Text { get; set; }

        public string? Note { get; set; }
        public ICollection<Vocabulary> Vocabularies { get; set; }
        public ICollection<CategoryTag> CategoryTags { get; set; }

    }
}