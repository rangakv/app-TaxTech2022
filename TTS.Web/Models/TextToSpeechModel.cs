using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TTS.Web.Models
{
    public class TextToSpeechModel
    {
        public TextToSpeechModel()
        {
            this.Neurals = new List<Neural>
            {
                new Neural{NeuralVoice="en-US-ChristopherNeural",Locale="English-US",Gender="Male" },
                new Neural{NeuralVoice="en-IN-PrabhatNeural",Locale="English-India",Gender="Male" },
                new Neural{NeuralVoice="fr-FR-HenriNeural",Locale="French",Gender="Male" },
                new Neural{NeuralVoice="en-PH-JamesNeural",Locale="English-Philippines",Gender="Male" },
                new Neural{NeuralVoice="en-GB-RyanNeural",Locale="British",Gender="Male" },
                new Neural{NeuralVoice="en-US-AshleyNeural",Locale="English-US",Gender="Female" },
                new Neural{NeuralVoice="en-IN-NeerjaNeural",Locale="English-India",Gender="Female" },
                new Neural{NeuralVoice="fr-CH-ArianeNeural",Locale="French",Gender="Female" },
                new Neural{NeuralVoice="fil-PH-BlessicaNeural",Locale="English-Philippines",Gender="Female" },
                new Neural{NeuralVoice="en-GB-SoniaNeural",Locale="English-British",Gender="Female" }
            };
        }
        [Required]
        public string Content { get; set; }

        public string SubscriptionKey { get; set; } = "< Subscription Key >";

        [DisplayName("Neural Selection :")]
        [Required]
        public string NeuralCode { get; set; } = "NA";       

        [DisplayName("Neural Selection :")]
        public string Gender { get; set; } = "Male";

        public List<Neural> Neurals { get; set; }

        public List<SelectListItem> LanguagePreference { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "NA", Text = "-- Select --" },
            new SelectListItem { Value = "English-US", Text = "English-US"  },
            new SelectListItem { Value = "English-India", Text = "English-India"  },
            new SelectListItem { Value = "French", Text = "French"  },
            new SelectListItem { Value = "English-Philippines", Text = "English-Philippines"  },
            new SelectListItem { Value = "English-British", Text = "English-British"  }
        };

        public string ProsodyRate { get; set; }
        public string PitchRate { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public int EmployeeId { get; set; }
        public bool PlayEmployeeVoice { get; set; }
        public string AudioPath { get; set; }
    }
    public class Neural
    {        
        public string NeuralVoice { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
    }
}
