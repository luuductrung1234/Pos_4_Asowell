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
        public decimal SaleValue { get; set; }      //sale_value
        public decimal TotalPriceNonDisc { get; set; }  // totalPrice_nonDisc
        public decimal Svc { get; set; }    // Svc
        public decimal Vat { get; set; }    // Vat
        public string SubEmpId { get; set; }    // subEmp_id
        public int Discount { get; set; }       // discount
        public int Pax { get; set; }        //Pax
    }
}
