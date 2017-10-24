using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class Order
    { 
        public string Name { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }

        public static explicit operator Order(string v)
        {
            throw new NotImplementedException();
        }
    }
    public class OrderData
    {
        public static ObservableCollection<Order> Orderlist = new ObservableCollection<Order>();
        
    }
}
