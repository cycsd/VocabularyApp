﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DictionaryService
{
    public class SimpleWordInfoDto
    {
        public IEnumerable<KeyValuePair> Categories { get; set; }
        public int WordId { get; set; }
        public string Text { get; set; }
        public IEnumerable<DefinitionInfoDto> PartOfSpeech { get; set; }
        public string Note { get; set; }

        public string PronounceAudioUrl { get; set; }
    }

    public class DefinitionInfoDto
    {
        public string PartOfSpeech { get; set; }
        public IEnumerable<string> Definitions { get; set; }
    }
}
