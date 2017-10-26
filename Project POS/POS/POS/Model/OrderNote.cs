using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class OrderNote
    {
        public string ordernote_id { get; set; }
        public string cus_id { get; set; }
        public string emp_id { get; set; }
        public int ordertable { get; set; }
        public DateTime ordertime { get; set; }
        public float total_price { get; set; }
        public float customer_pay { get; set; }
        public float pay_back { get; set; }
    }
}
