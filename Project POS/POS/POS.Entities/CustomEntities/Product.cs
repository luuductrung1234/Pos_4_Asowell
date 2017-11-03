using System;
using System.Windows.Media.Imaging;

namespace POS.Entities
{
    public partial class Product
    {
        public static BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri(@"/Images/" + filename, UriKind.RelativeOrAbsolute));
        }
    }
}
