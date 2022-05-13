using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TTS.Business
{
    public class TextToSpeechService : ITextToSpeech
    {
        /// <summary>
        /// Translate text to speech
        /// </summary>
  
  
        /// <param name="token">Authentication token</param>
        /// <param name="key">Azure subscription key</param>
        /// <param name="content">Text content for speech</param>
        /// <param name="lang">Speech conversion language</param>
        /// <returns></returns>
        public async Task<byte[]> TranslateText(string token, string key, string content, string lang, string serviceuri)
        {
            //Request url for the speech api.
            //string uri = "https://eastus.tts.speech.microsoft.com/cognitiveservices/v1";
            //Generate Speech Synthesis Markup Language (SSML)
            var requestBody = this.GenerateSsml(lang, "Female", lang, content, "en-US-ChristopherNeural");

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(serviceuri);
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("X-Microsoft-OutputFormat", "audio-16khz-64kbitrate-mono-mp3");
                request.Content = new StringContent(requestBody, Encoding.UTF8, "text/plain");
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", "application/ssml+xml");
                request.Headers.Add("User-Agent", "TexttoSpeech");
                var response = await client.SendAsync(request);
                var httpStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                Stream receiveStream = httpStream;
                byte[] buffer = new byte[32768];

                using (Stream stream = httpStream)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        byte[] waveBytes = null;
                        int count = 0;
                        do
                        {
                            byte[] buf = new byte[1024];
                            count = stream.Read(buf, 0, 1024);
                            ms.Write(buf, 0, count);
                        } while (stream.CanRead && count > 0);

                        waveBytes = ms.ToArray();

                        return waveBytes;
                    }
                }
            }
        }

        private string GenerateSsml(string lang, string gender, object p, string messageBody, string neural)
        {
            string body = @"<speak version='1.0' xml:lang='" + lang + "'><voice xml:gender='Male' xml:lang='" + lang + "' name='" + neural + "'>" + messageBody + "</voice></speak>";
            return body;
        }
    }
}
