using DataLayer.EfClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService.Mapping
{
    public static class DictionaryDtoMapping
    {
        public static Word ProjectToWord(this VocabularyDto wordDto)
        {
            var word = new Word
            {
                Text = wordDto.word,
                Vocabularies = wordDto.meanings
                .Select(mean => new Vocabulary
                {
                    PartOfSpeech = mean.partOfSpeech,
                    IPA = wordDto.phonetics.FirstOrDefault(ph => !string.IsNullOrWhiteSpace(ph.text))?.text,
                    Pronounce = wordDto.phonetics.FirstOrDefault(ph => !string.IsNullOrWhiteSpace(ph.audio))?.audio,
                    Definitions = mean.definitions
                                        .Select(def =>
                                        new Define
                                        {
                                            Definition = def.definition,
                                            Example = def.example
                                        }).ToList()
                }).ToList(),

            };
            return word;
        }

    }
}
