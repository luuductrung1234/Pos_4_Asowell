using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Entities
{
    public partial class OrderTemp
    {
        public decimal TotalPriceNonDisc { get; set; }  // totalPrice_nonDisc
        public string SubEmpId { get; set; }    // subEmp_id
        public int Discount { get; set; }       // discount
    }
}
