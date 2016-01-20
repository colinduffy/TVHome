using System.Resources;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System;

namespace TVHome.SpeechRecognition
{
    static class Support
    {
        public static string[] GetSupportedWords()
        {
            List<String> supportedWords = new List<String>();
            ResourceSet resourceSet = SupportedWords.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            
            foreach (DictionaryEntry entry in resourceSet)
            {
                supportedWords.Add(entry.Value.ToString());
            }
           
            return supportedWords.ToArray();
        }
    }
}
