
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class Ingredient
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

    public class IngredientData
    {
        public static List<Ingredient> IgdList
        {
            get
            {
                return new List<Ingredient>
                {
                    new Ingredient {Igd_id="IGD0000001", Warehouse_id="WAR0000001", Name="Cocacola", Info="", Usefor=0, Igd_type="dry", Unit_buy="box", Standard_price=120000, Deleted=false },
                    new Ingredient {Igd_id="IGD0000002", Warehouse_id="WAR0000002", Name="Coffee bean", Info="", Usefor=0, Igd_type="dry", Unit_buy="package", Standard_price=90000, Deleted=false },
                    new Ingredient {Igd_id="IGD0000003", Warehouse_id="WAR0000003", Name="Aquafina", Info="", Usefor=0, Igd_type="dry", Unit_buy="box", Standard_price=110000, Deleted=false },
                    new Ingredient {Igd_id="IGD0000004", Warehouse_id="WAR0000004", Name="Heniken beer", Info="", Usefor=0, Igd_type="dry", Unit_buy="box", Standard_price=200000, Deleted=false },
                    new Ingredient {Igd_id="IGD0000005", Warehouse_id="WAR0000005", Name="Tiger beer", Info="", Usefor=0, Igd_type="dry", Unit_buy="box", Standard_price=180000, Deleted=false },
                    new Ingredient {Igd_id="IGD0000006", Warehouse_id="WAR0000006", Name="Grap beer", Info="", Usefor=0, Igd_type="dry", Unit_buy="box", Standard_price=300000, Deleted=false },
                };
            }
        }

    }
}
