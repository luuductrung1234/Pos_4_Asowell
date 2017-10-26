using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class Customer
    {
        public string Cus_id { get; set; }
        public string Name { get; set; }
        public string Phone { get;set; }
        public string Email { get; set; }
        public int Discount { get; set; }
        public bool Deleted { get; set; }
    }
    
    public class CustomerData
    {
        public static List<Customer> CusList
        {
            get
            {
                return new List<Customer>
                {
                    new Customer {Cus_id = "CUS0000000", Name="Guest", Phone="", Email="", Discount=0, Deleted=false },
                    new Customer {Cus_id = "CUS0000001", Name="Luu Duc Trung", Phone="0903434234", Email="luuductrung@gmail.com", Discount=20, Deleted=false },
                    new Customer {Cus_id = "CUS0000002", Name="Nguyen Hoang Nam", Phone="0903444444", Email="hoangnam11@gmail.com", Discount=10, Deleted=false },
                    new Customer {Cus_id = "CUS0000003", Name="Le Duc Anh", Phone="09034222222", Email="leducanh@gmail.com", Discount=20, Deleted=false },
                    new Customer {Cus_id = "CUS0000004", Name="Than Duc Huy", Phone="0903434111", Email="duchuy_than@gmail.com", Discount=5, Deleted=false },
                    
                };
            }
        }
    }
}
