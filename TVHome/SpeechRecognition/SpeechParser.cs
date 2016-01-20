using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHome.SpeechRecognition
{
    class SpeechParser
    {
        public List<String> ParsedWords;

        public SpeechParser()
        {
            ParsedWords = new List<String>();
        }

        public void ParseSpeech(string word)
        {
            ParsedWords.Add(word);
        }
    }
}
