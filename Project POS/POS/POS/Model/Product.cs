using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace POS.Model
{
    public class Product
    {
        public string Product_id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public float Price { get; set; }
        public byte Is_todrink { get; set; }
        public bool Deleted { get; set; }
        public string ImageLink { get; set; }
        public BitmapImage ImageData
        {
            get { return LoadImage(this.ImageLink); }
        }



        public static BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri(@"/Images/" + filename, UriKind.RelativeOrAbsolute));
        }
    }





    public class ProductData
    {
        public static Product[] PList = new Product[]
          {
                new Product {Product_id = "P000000001", Name="Cocacola",Price=100000, ImageLink="h2.jpg" },
                new Product {Product_id = "P000000002", Name="CapuchinoP",Price=150000, ImageLink="h2.jpg" },
                new Product {Product_id = "P000000003", Name="Water",Price=100000, ImageLink="h3.jpg" },
                new Product {Product_id = "P000000004", Name="Heniken Beer",Price=200000, ImageLink="h4.jpg" },
                new Product {Product_id = "P000000005", Name="Tiger Beer",Price=580000, ImageLink="h5.jpg" },
                new Product {Product_id = "P000000006", Name="Cup beer",Price=33000, ImageLink="h6.jpg" },
          };
        public static BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri(@"/Images/" + filename, UriKind.RelativeOrAbsolute));
        }
    }
}
