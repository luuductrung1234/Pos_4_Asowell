using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Helper.PrintHelper.Model
{
    public class SalaryNoteForReport
    {
        public string SnId { get; set; } // sn_id (Primary key) (length: 10)
        public string EmpId { get; set; } // emp_id (length: 10)
        public string EmpName { get; set; }
        public string DatePay { get; set; } // date_pay
        public decimal SalaryValue { get; set; } // salary_value
        public double WorkHour { get; set; } // work_hour
        public int ForMonth { get; set; } // for_month
        public int ForYear { get; set; } // for_year
        public string IsPaid { get; set; } // is_paid
    }
}
