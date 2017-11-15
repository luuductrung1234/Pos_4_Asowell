using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Helper.PrintHelper.Model
{
    public class OrderForPrint
    {
        public string No { get; set; }
        public string Casher { get; set; }
        public string Customer { get; set; }
        public int Table { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (var detail in OrderDetails)
                {
                    total += detail.Amt;
                }
                return total;
            }
        }
        public decimal CustomerPay { get; set; }
        public decimal PayBack
        {
            get
            {
                return CustomerPay - TotalPrice;
            }
        }

        public List<OrderDetailsForPrint> OrderDetails
        {
            get
            {
                return new List<OrderDetailsForPrint>()
                {
                    new OrderDetailsForPrint(){SelectedStats ="Stater",Quan=2,ProductName="Pepsi",ProductPrice=25,Amt=50},
                    new OrderDetailsForPrint(){SelectedStats ="Stater",Quan=1,ProductName="Coca Cola",ProductPrice=25,Amt=25},
                    new OrderDetailsForPrint(){SelectedStats ="Stater",Quan=1,ProductName="French Fries",ProductPrice=35,Amt=35},
                    new OrderDetailsForPrint(){SelectedStats ="Stater",Quan=2,ProductName="Honey Butter Bread",ProductPrice=70,Amt=170},
                    new OrderDetailsForPrint(){SelectedStats ="Stater",Quan=3,ProductName="Comma Pizza",ProductPrice=50,Amt=150},
                    new OrderDetailsForPrint(){SelectedStats ="Stater",Quan=1,ProductName="Soda",ProductPrice=25,Amt=25},
                };
            }
        }




        // Receipt Meta
        public Dictionary<string, string> getMetaReceiptInfo()
        {
            return new Dictionary<string, string>()
            {
                { "No", No},
                { "Table", Table.ToString()},
                { "Date", Date.ToString() },
                { "Casher", Casher},
                { "Customer", Customer}
            };
        }
        public string[] getMetaReceiptTable()
        {
            return new string[]
            {
                "Product Price",
                "Quan",
                "Price",
                "Amt"
            };
        }

        // Kitchen Meta
        public string[] getMetaKitchenTable()
        {
            return new string[]
            {
                "Chair Number",
                "Quan",
                "Product Name",
            };
        }

        // Bar Meta
        public string[] getMetaBarTable()
        {
            return new string[]
            {
                "Chair Number",
                "Quan",
                "Product Name",
            };
        }
    }
}
