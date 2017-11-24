using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Entities
{
    public partial class OrderNote
    {
        public int paymentMethod { get; set; }    // pay_method
        public decimal TotalPriceNonTax { get; set; }  // totalPrice_nonTax
        public string SubEmpId { get; set; }    // subEmp_id
        public int Discount { get; set; }       // discount
    }
}
