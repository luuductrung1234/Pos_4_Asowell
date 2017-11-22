using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Helper.PrintHelper.Model
{
    public class OrderEntityForReport
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quan { get; set; }
        public int BillCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
