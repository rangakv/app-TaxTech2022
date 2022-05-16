using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTS.Data.Models;

namespace TTS.Web.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool NonStandard { get; set; }
        public string AudioPath { get; set; }
        public bool Optout { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDt { get; set; }
    }
}
