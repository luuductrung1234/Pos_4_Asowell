using System.Collections.ObjectModel;

namespace POS.Helper.PrintHelper.Model
{
    public class OrderDetailsForPrint
    {
        // Main data (data for Receipt pringting)
        public int Quan { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Amt
        {
            get
            {
                return ProductPrice* Quan;
            }
        }


        // Extend data (data for other printing)
        public string ProductId { get; set; }
        public int ProductType { get; set;  }
        public int ChairNumber { get; set; }
        public string Note { get; set; }
        public string SelectedStats { get; set; }
        public static ObservableCollection<string> StatusItems
        {
            get
            {
                return new ObservableCollection<string>()
                {
                    "Stater",
                    "MainCost",
                    "Dessert",
                    "Beverage",
                };
            }
        }
    }
}
