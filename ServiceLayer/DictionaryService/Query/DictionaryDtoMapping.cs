using DataLayer.EfClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService.Query
{
    public static class DictionaryDtoMapping
    {
        public static Word MapToWord(this VocabularyDto wordDto)
        {
            var word = new Word
            {
                CategoryTags = new List<CategoryTag>(),
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
        public static IQueryable<VocabularyDto> MapToVocabulary(this IQueryable<Word> source)
        {
            return source.Select(w => new VocabularyDto
            {
                wordId = w.WordId,
                word = w.Text,
                note = w.Note,
                phonetics = w.Vocabularies.Select(vocabulary =>
                             new Phonetic
                             {
                                 text = vocabulary.IPA,
                                 audio = vocabulary.Pronounce,
                             }).ToList(),
                meanings = (from vocabulary in w.Vocabularies
                            select new Meaning
                            {
                                partOfSpeech = vocabulary.PartOfSpeech,
                                definitions = (from define in vocabulary.Definitions
                                               select new Definition
                                               {
                                                   definition = define.Definition,
                                                   example = define.Example,
                                               }).ToList(),
                            }).ToList()

            });
        }
        public static IQueryable<SimpleWordInfoDto> MapWordToSimpleWordDto(this IQueryable<Word> words)
            => words.Select(MapWordToSimpleWordDto());

        public static SimpleWordInfoDto MapWordToSimpleWordDto(this Word word)
            => MapWordToSimpleWordDto().Compile()(word);
        private static Expression<Func<Word, SimpleWordInfoDto>> MapWordToSimpleWordDto()
            => word
            => new SimpleWordInfoDto
            {
                Categories = word.CategoryTags.MapToKeyValuePair(),
                WordId = word.WordId,
                Text = word.Text,
                Note = word.Note,
                PartOfSpeech = word.Vocabularies.Select(v =>
                new DefinitionInfoDto
                {
                    PartOfSpeech = v.PartOfSpeech,
                    Definitions = v.Definitions.Select(d => d.Definition)
                }),
                PronounceAudioUrl = word.Vocabularies.Select(v => v.Pronounce)
                                .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p))
                                ?? string.Empty,
            };



    }
}
