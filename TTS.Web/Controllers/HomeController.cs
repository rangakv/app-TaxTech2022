using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
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
using TTS.Data.Models;
using TTS.Security;
using TTS.Web.Models;

namespace TTS.Web.Controllers
{
    [Authorize(Policy = "TaxTechUser")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITextToSpeech _textToSpeech;
        private readonly IKeyVaultService _keyVaultService;
        private readonly IEmployeeService _employeeService;
        private readonly IBlobService _blobService;
        private ttsdbContext _ttsDBContext;
        private IConfiguration _config;
        
        public HomeController(ILogger<HomeController> logger, IConfiguration config, 
            ITextToSpeech textToSpeech, IKeyVaultService keyVaultService, 
            IEmployeeService employeeService, ttsdbContext context, IBlobService blobService)
        {
            _logger = logger;
            _textToSpeech = textToSpeech;
            _config = config;
            _keyVaultService = keyVaultService;
            _employeeService = employeeService;
            _ttsDBContext = context;
            _blobService = blobService;
        }

        public IActionResult Index()
        {
            var empList = _employeeService.GetEmployees();
            TextToSpeechModel model = new TextToSpeechModel();
            model.EmployeeList = PrepareEmployeeList(empList);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(TextToSpeechModel model)
        {
            var IsEmployeeVoiceAvailable = false;
            var empList = _employeeService.GetEmployees();
            model.EmployeeList = PrepareEmployeeList(empList);
            if (model.PlayEmployeeVoice)
            {
                var employee = _employeeService.GetEmployeeVoiceDetails(model.Content);
                if (employee != null && !string.IsNullOrEmpty(employee.AudioPath) && employee.NonStandard && !employee.Optout)
                {
                    model.AudioPath = employee.AudioPath;
                    IsEmployeeVoiceAvailable = true;
                }
            }
            if (!IsEmployeeVoiceAvailable)
            {
                string tokenUri = _config.GetValue<string>("AzureTTS:TokenUri");
                string keyVaultUri = _config.GetValue<string>("AzureTTS:KeyVaultUri");
                AzAppRegistration taxTechApp = PrepareAppRegDetails(_config);
                var key = _keyVaultService.GetSecret("ttskey", keyVaultUri, taxTechApp);
                Authentication auth = new Authentication(tokenUri, key);
                var token = auth.GetAccessToken();
                ViewBag.token = token;
                ViewBag.key = key;
                ViewBag.content = model.Content;
                ViewBag.gender = model.Gender;
                ViewBag.prosodyrate = GetProsodyRate(model.ProsodyRate);
                ViewBag.pitchrate = GetPitchRate(model.PitchRate);
                ViewBag.neuralCode = GetNeuralVoice(model.Gender, model.NeuralCode, model);
                ViewBag.serviceuri = _config.GetValue<string>("AzureTTS:ServiceUri");
                model.PlayEmployeeVoice = false;
            }
            return View(model);
        }
        public async Task<FileResult> Translate(string token, string key, string content, string gender, string neuralCode, string prosodyrate, string pitchrate, string serviceuri)
        {            
            var audioStreamBytes = await _textToSpeech.TranslateText(token, key, content, gender, neuralCode, prosodyrate, pitchrate, serviceuri);
            Stream s = new MemoryStream(audioStreamBytes);
            return File(s, "audio/mpeg");
        }
        public async Task<FileResult> PlayEmployeeVoice(string audiopath)
        {
            string containerName = audiopath.Split('/')[1].ToString();
            string blobName = audiopath.Split('/')[2].ToString();
            string blobCon = _config.GetValue<string>("AzureTTS:BlobUri");
            var audioStreamBytes = await _blobService.DownLoadBlobStream(containerName, blobName, blobCon);
            Stream s = new MemoryStream(audioStreamBytes);
            return File(s, "audio/mpeg");
        }

        public PartialViewResult GetEmployeeDetails(int id)
        {
            var model = _employeeService.GetEmployeeDetails(id);
            EmployeeModel employeeModel = new EmployeeModel()
            {
                EmployeeId = model.EmployeeId,
                EmployeeName = model.EmployeeName,
                PrefferedName=model.PrefferedName,
                AudioPath = model.AudioPath,
                NonStandard = model.NonStandard,
                Optout = model.Optout
            };
            
            return PartialView("_employee", employeeModel);
        }
        [HttpPost]
        public IActionResult OptoutEmployee(string eid, bool optout)
        {
            _employeeService.OptoutEmployee(Convert.ToInt32(eid), optout);
            return Json(new { success = true });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private AzAppRegistration PrepareAppRegDetails(IConfiguration config)
        {
            AzAppRegistration appReg = new AzAppRegistration
            {
                Instance = config.GetValue<string>("AzureAd:Instance"),
                TenantId = config.GetValue<string>("AzureAd:TenantId"),
                ClientId = config.GetValue<string>("AzureAd:ClientId"),
                clientSecret = config.GetValue<string>("AzureAd:clientSecret")
            };
            return appReg;
        }
        private string GetNeuralVoice(string gender,string locale, TextToSpeechModel model)
        {
            return model.Neurals.Where(n => n.Gender == gender && n.Locale == locale).Select(s => s.NeuralVoice).FirstOrDefault();
        }
        private string GetProsodyRate(string prosody)
        {
            var prosodyRate = ((Convert.ToDecimal(prosody) * 100) - 100);
            return string.Format("{0}%", prosodyRate.ToString());
        }
        private string GetPitchRate(string pitch)
        {
            var pitchRate = (((Convert.ToDecimal(pitch) * 100) - 100) / 2);
            return string.Format("{0}%", pitchRate.ToString());
        }
        private List<SelectListItem> PrepareEmployeeList(List<Employee> employees)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem { Text = "--Select Employee--", Value = "NA" });
            foreach (var item in employees)
            {
                selectListItems.Add(new SelectListItem { Text = item.EmployeeName, Value = item.EmployeeId.ToString() });
            }
            return selectListItems;
        }

    }
}
