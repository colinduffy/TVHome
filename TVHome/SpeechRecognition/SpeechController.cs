using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVHome;
using TVHome.Views;

namespace TVHome.SpeechRecognition
{
    class SpeechController
    {
        private Boolean Active;

        public SpeechController()
        {
            Active = false;
        }

        public void SpeechReceived(List<string> parsedWords)
        {
            string lastWord = parsedWords.Last();
            
            if(lastWord.Equals(SupportedWords.Colin))
            {
                Active = true;
                return;
            }
            else if(lastWord.Equals(SupportedWords.Basketball) && Active)
            {
                Switcher.Switch(new Timer());
            }

            Active = false;
        }
    }
}
