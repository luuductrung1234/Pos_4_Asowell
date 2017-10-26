using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class ProductDetails
    {
        public string Pdetail_id { get; set; }
        public string Product_id { get; set; }
        public string Igd_id { get; set; }
        public float Quan { get; set; }
        public string Unit_use { get; set; }
    }

    public class ProductDetailsData
    {
        public static List<ProductDetails> PDetailsList
        {
            get
            {
                return new List<ProductDetails>
                {
                    new ProductDetails { Pdetail_id = "PD00000001", Product_id="P000000001", Igd_id="IGD0000001", Quan=1, Unit_use="bottle"},
                    new ProductDetails { Pdetail_id="PD00000002", Product_id="P000000002", Igd_id="IGD0000002", Unit_use="g", Quan=15},
                    new ProductDetails { Pdetail_id="PD00000003", Product_id="P000000003", Igd_id="IGD0000003", Unit_use="bottle", Quan=1 },
                    new ProductDetails { Pdetail_id="PD00000004", Product_id="P000000004", Igd_id="IGD0000004", Unit_use="bottle", Quan=1 },
                    new ProductDetails { Pdetail_id="PD00000005", Product_id="P000000005", Igd_id="IGD0000005", Unit_use="bottle", Quan=1 },
                    new ProductDetails { Pdetail_id="PD00000006", Product_id="P000000006", Igd_id="IGD0000006", Unit_use="grapshot", Quan=1 },
                };
            }
        }
    }
}
