using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace POS.Model
{
    public class Item
    {
        private string _Name;
        public int Price { get; set; }
        private BitmapImage _ImageData { get; set; }
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }
        public BitmapImage ImageData
        {
            get { return this._ImageData; }
            set { this._ImageData = value; }
        }
    } 
    public class ItemData
    {
        public static Item[] ilist = new Item[]
          {
                new Item {Name="Cocacola",Price=100000, ImageData=LoadImage("h2.jpg") },
                new Item {Name="CapuchinoP",Price=150000, ImageData=LoadImage("h2.jpg") },
                new Item {Name="Water",Price=100000, ImageData=LoadImage("h3.jpg") },
                new Item {Name="Heniken Beer",Price=200000, ImageData=LoadImage("h4.jpg") },
                new Item {Name="Tiger Beer",Price=580000, ImageData=LoadImage("h5.jpg") },
                new Item {Name="Cup beer",Price=33000, ImageData=LoadImage("h6.jpg") },
                new Item {Name="Tiger Beer",Price=690000, ImageData=LoadImage("h5.jpg") },
          };
        public static BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri(@"/Images/" + filename, UriKind.RelativeOrAbsolute));
        }
    } 
}
