namespace DataLayer.EfClasses
{
    public class Word
    {
        public int WordId { get; set; }

        public ICollection<Vocabulary> Vocabularies { get; set; }
    }
}