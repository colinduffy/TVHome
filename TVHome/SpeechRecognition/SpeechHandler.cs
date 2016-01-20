using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Windows.Threading;
using System.Windows;

namespace TVHome.SpeechRecognition
{
    class SpeechHandler
    {
        private SpeechController SpeechController;

        public SpeechHandler()
        {
            InitializeSpeechRecognition();
            SpeechParser = new SpeechParser();
        }

        public SpeechParser SpeechParser { private set; public get; }

        private void InitializeSpeechRecognition()
        {
            SpeechRecognitionEngine SpeechRecognizer = new SpeechRecognitionEngine();
            Choices sList = new Choices(Support.GetSupportedWords());
            try
            {
                SpeechRecognizer.RequestRecognizerUpdate();
                SpeechRecognizer.LoadGrammar(new Grammar(new GrammarBuilder(sList)));
                SpeechRecognizer.SpeechRecognized += SpeechRecognizedEvent;
                SpeechRecognizer.SetInputToDefaultAudioDevice();
                SpeechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
        }

        private void SpeechRecognizedEvent(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show(e.Result.Text);
            SpeechParser.ParseSpeech(e.Result.Text);
            SpeechController.SpeechReceived(SpeechParser.ParsedWords);
        }
    }
}
