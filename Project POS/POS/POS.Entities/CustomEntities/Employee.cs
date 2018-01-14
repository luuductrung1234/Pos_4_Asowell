using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Entities
{
    public enum EmployeeRole
    {
        Ministering = 0,
        Bar = 1,
        Kitchen = 2,
        Stock = 3,
        Cashier = 4
    }

    public partial class Employee
    {
        public string EmpCode { get; set; }    //emp_code
        public string DecryptedPass { get; set; }
        public string DecryptedCode { get; set; }
    }
}
