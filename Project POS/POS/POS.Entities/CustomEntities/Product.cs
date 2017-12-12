using System;
using System.Windows.Media.Imaging;

namespace POS.Entities
{
    public partial class Product
    {
        public BitmapImage ImageData
        {
            get { return LoadImage(ImageLink); }
        }

        public static BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri(@"/Images/Products/" + filename, UriKind.RelativeOrAbsolute));
        }
    }
}
