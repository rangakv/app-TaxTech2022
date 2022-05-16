using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TTS.Data.Models
{
    public partial class Employee
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
        public string PrefferedName { get; set; }
    }
}
