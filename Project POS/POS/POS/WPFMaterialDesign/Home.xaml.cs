using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POS.WPFMaterialDesign
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/luuductrung1234/Pos_4_Asowell");
        }

        private void EmailButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto://luuductrung@itcomma.com");
        }

        private void ExploreButton_OnClick(object sender, RoutedEventArgs e)
        {
            //play a demo video
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void DetailsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/Asowel-Rooftop-Cafe-Dining-185441705356870/");
        }
    }
}
