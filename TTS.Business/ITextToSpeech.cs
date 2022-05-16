using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TTS.Business
{
    public interface ITextToSpeech
    {
        Task<byte[]> TranslateText(string token, string key, string content, string gender, string neuralCode, string prosodyrate, string pitchrate, string serviceuri);
    }
}
