using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Oasis.Communication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureSpeechServiceController : ControllerBase
    {
        public AzureSpeechServiceController() { }

        [HttpPost]
        [Route("SpeechToText")]
        public async Task<string> ConvertSpeechToText()
        {
            var speechConfig = SpeechConfig.FromSubscription("42a16b22bcac4c38a90448d7a3aaf024", "eastus");
            //using var audioConfig = AudioConfig.FromWavFileInput("C:\\Karthikeyan\\SampleAudioFile.wav");//From external audio file
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();//From our own microphone
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
            var result = await speechRecognizer.RecognizeOnceAsync();
            return await Task.FromResult(result.Text);
        }

        [HttpPost]
        [Route("TextToSpeech")]
        public async Task<string> TextToSpeech()
        {
            var speechConfig = SpeechConfig.FromSubscription("42a16b22bcac4c38a90448d7a3aaf024", "eastus");
            string audioFileName = "outputAudio.wav";
            using var audioConfig = AudioConfig.FromWavFileOutput("C:\\Karthikeyan\\" + audioFileName);
            using var speechSynthesizer = new SpeechSynthesizer(speechConfig, audioConfig);
            var result = await speechSynthesizer.SpeakTextAsync("Hello Karthikeyan! Welcome to azure cognitive services");
            return await Task.FromResult(result.ResultId);
        }
    }
}
