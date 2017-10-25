using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    class Employee
    {
        public string Emp_id { get; set; }
        public string Manager { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public DateTime Startday { get; set; }
        public int Hour_wage { get; set; }
        public string Addr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Emp_role { get; set; }
        public bool Deleted { get; set; }
    }
}
