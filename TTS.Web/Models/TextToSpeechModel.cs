using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TTS.Web.Models
{
    public class TextToSpeechModel
    {
        public string Content { get; set; }

        public string SubscriptionKey { get; set; } = "< Subscription Key >";

        [DisplayName("Language Selection :")]
        public string LanguageCode { get; set; } = "NA";

        [DisplayName("Neural Selection :")]
        public string NeuralEndPoint{ get; set; } = "NA";

        public List<SelectListItem> LanguagePreference { get; set; } = new List<SelectListItem>
        {
        new SelectListItem { Value = "NA", Text = "-Select-" },
        new SelectListItem { Value = "en-US", Text = "English (United States)"  },
        new SelectListItem { Value = "en-IN", Text = "English (India)"  },
        new SelectListItem { Value = "ta-IN", Text = "Tamil (India)"  },
        new SelectListItem { Value = "hi-IN", Text = "Hindi (India)"  },
        new SelectListItem { Value = "te-IN", Text = "Telugu (India)"  }
        };

        public List<SelectListItem> NeuralPreference { get; set; } = new List<SelectListItem>
        {
        new SelectListItem { Value = "NA", Text = "-Select-" },
        new SelectListItem { Value = "https://centralus.tts.speech.microsoft.com/cognitiveservices/v1", Text = "US Central"  },
        new SelectListItem { Value = "https://eastus.tts.speech.microsoft.com/cognitiveservices/v1", Text = "US East"  },
        new SelectListItem { Value = "https://chinaeast2.tts.speech.azure.cn/cognitiveservices/v1", Text = "China East 2"  },
        new SelectListItem { Value = "https://francecentral.tts.speech.microsoft.com/cognitiveservices/v1", Text = "France Central"  },
        new SelectListItem { Value = "https://centralindia.tts.speech.microsoft.com/cognitiveservices/v1", Text = "India Central"  },
        new SelectListItem { Value = "https://southeastasia.tts.speech.microsoft.com/cognitiveservices/v1", Text = "Southeast Asia"  },
        new SelectListItem { Value = "https://uksouth.tts.speech.microsoft.com/cognitiveservices/v1", Text = "UK South"  }
        };
    }
}
