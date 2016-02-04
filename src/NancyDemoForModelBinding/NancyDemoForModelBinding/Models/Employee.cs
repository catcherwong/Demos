using System.Collections.Generic;

namespace NancyDemoForModelBinding.Models
{
    public class Employee
    {
        public Employee()
        {
            this.EmployeeNumber = "Num1";
            this.EmployeeName = "Catcher8";
            this.EmployeeAge = 18;

        }

        public string EmployeeNumber { get; set; }

        public string EmployeeName { get; set; }

        public int EmployeeAge { get; set; }

        public List<string> EmployeeHobby { get; set; }
    }
}