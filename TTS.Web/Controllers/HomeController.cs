using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using TTS.Business;
using TTS.Web.Models;

namespace TTS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITextToSpeech _textToSpeech;

        public HomeController(ILogger<HomeController> logger, ITextToSpeech textToSpeech)
        {
            _logger = logger;
            _textToSpeech = textToSpeech;
        }

        public IActionResult Index()
        {
            TextToSpeechModel model = new TextToSpeechModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(TextToSpeechModel model)
        {
            string tokenUri = "https://eastus.api.cognitive.microsoft.com/sts/v1.0/issuetoken";
            string key = "5686bffe55e1490e965d0036f299ed29";
            Authentication auth = new Authentication(tokenUri, key);
            var token = auth.GetAccessToken();
            ViewBag.token = token;
            ViewBag.key = key;
            ViewBag.content = model.Content;
            ViewBag.languagecode ="en-US";
            ViewBag.serviceuri = model.NeuralEndPoint;
            return View(model);
        }
        public async Task<IActionResult> Translate(string token, string key, string content, string lang, string serviceuri)
        {            
            var audioStreamBytes = await _textToSpeech.TranslateText(token, key, content, lang, serviceuri);
            Stream s = new MemoryStream(audioStreamBytes);
            return File(s, "audio/mpeg");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
    }
}
