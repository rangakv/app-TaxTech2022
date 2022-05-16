using System;
using System.Collections.Generic;
using System.Text;
using TTS.Data.Models;

namespace TTS.Business
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeDetails(int id);
        Employee GetEmployeeVoiceDetails(string name);
        void OptoutEmployee(int id, bool optOut);
        void DeleteEmployee(int id);
    }
}
