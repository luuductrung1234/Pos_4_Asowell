using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    class Customer
    {
        public string Cus_id { get; set; }
        public string Name { get; set; }
        public string Phone { get;set; }
        public string Email { get; set; }
        public int Discount { get; set; }
        public bool Deleted { get; set; }
    }
}
