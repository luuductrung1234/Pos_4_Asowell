
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    class Ingredient
    {
        public string Igd_id { get; set; }
        public string Warehouse_id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public byte Usefor { get; set; }
        public string Igd_type { get; set; }
        public string Unit_buy { get; set; }
        public float Standard_price { get; set; }
        public bool Deleted { get; set; }
    }
}
