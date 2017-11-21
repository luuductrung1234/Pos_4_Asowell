using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Helper.PrintHelper.Model
{
    public class SalaryEntityForReport
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double WorkHour { get; set; }
        public decimal Salary { get; set; }
    }
}
