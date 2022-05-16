using System;
using System.Collections.Generic;
using System.Text;

namespace TTS.Security.Security
{
    public interface ITextToSpeechAuth
    {
        string GetAccessToken();
    }
}
