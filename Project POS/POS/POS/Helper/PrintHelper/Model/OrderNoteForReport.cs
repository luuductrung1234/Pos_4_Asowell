using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Helper.PrintHelper.Model
{
    public class OrderNoteForReport
    {
        public string OrdernoteId { get; set; } // ordernote_id (Primary key) (length: 10)
        public string CusId { get; set; } // cus_id (length: 10)
        public string EmpId { get; set; } // emp_id (length: 10)
        public int Ordertable { get; set; } // ordertable
        public System.DateTime Ordertime { get; set; } // ordertime
        public decimal TotalPrice { get; set; } // total_price
        public decimal CustomerPay { get; set; } // customer_pay
        public decimal PayBack { get; set; } // pay_back
        public string payMethod { get; set; }
    }
}
