using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Helper.PrintHelper.Model
{
    public class OrderNoteDetailsForReport
    {
        public string OrdernoteId { get; set; } // ordernote_id (Primary key) (length: 10)
        public string ProductId { get; set; } // product_id (Primary key) (length: 10)
        public string ProductName { get; set; }
        public string EmpId { get; set; }
        public int TableNumber { get; set; }
        public DateTime OrderTime { get; set; }
        public int Quan { get; set; } // quan
        public decimal TotalPrice { get; set; }
        public string PayMethod { get; set; }
        
    }
}
