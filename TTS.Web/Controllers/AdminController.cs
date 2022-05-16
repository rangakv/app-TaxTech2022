using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTS.Business;
using TTS.Data.Models;

namespace TTS.Web.Controllers
{
    [Authorize(Policy = "TaxTechAdmin")]
    public class AdminController : Controller
    {
        private ttsdbContext _ttsDBContext;
        private readonly IEmployeeService _employeeService;
        public AdminController(IEmployeeService employeeService, ttsdbContext context)
        {
            _ttsDBContext = context;
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            var empList = _employeeService.GetEmployees();
            return View(empList);
        }
        public int DeleteAudio(int id)
        {
            _employeeService.DeleteEmployee(id);
            return 0;
        }
    }
}
