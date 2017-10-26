using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class Table
    {
        public int TableNumber { get; set; }
        public int ChairAmount { get; set; }
        public Order TableOrder { get; set; }
        public List<OrderProduct> TableOrderProduct { get; set; }   // OrderNote inside OrderProduct
        public Employee EmpRespond { get; set; }
        public Customer CusOwner { get; set; }

    }

}
