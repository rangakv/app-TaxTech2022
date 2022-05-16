using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTS.Data.Models;


namespace TTS.Business
{
    public class EmployeeService : IEmployeeService
    {
        private ttsdbContext _ttsDBContext;
        public EmployeeService(ttsdbContext context)
        {
            _ttsDBContext = context;
        }
        public List<Employee> GetEmployees()
        {
            return _ttsDBContext.Employee.ToList();
        }
        public Employee GetEmployeeDetails(int id)
        {
            return _ttsDBContext.Employee.Where(e => e.EmployeeId == id).FirstOrDefault();
        }
        public Employee GetEmployeeVoiceDetails(string name)
        {
            return _ttsDBContext.Employee.Where(e => e.EmployeeName == name).FirstOrDefault();
        }

        public void OptoutEmployee(int id, bool optOut)
        {
            var employee = _ttsDBContext.Employee.Where(e => e.EmployeeId == id).FirstOrDefault();
            if(employee != null)
            {
                employee.Optout = optOut;
                _ttsDBContext.SaveChanges();
            }
        }
    }
}
