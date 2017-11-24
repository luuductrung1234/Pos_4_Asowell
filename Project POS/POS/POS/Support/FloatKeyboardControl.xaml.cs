using System;
using System.Collections.Generic;
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

namespace POS.Support
{
    /// <summary>
    /// Interaction logic for FloatKeyboardControl.xaml
    /// </summary>
    public partial class FloatKeyboardControl : UserControl
    {
        public FloatKeyboardControl()
        {
            InitializeComponent();
        }


        private void BtnDeleteInput_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text = "";
        }

        private void BtnOne_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "1";
        }

        private void BtnTwo_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "2";
        }

        private void BtnThree_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "3";
        }

        private void BtnFour_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "4";
        }

        private void BtnFive_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "5";
        }

        private void BtnSix_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "6";
        }

        private void BtnSeven_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "7";
        }

        private void BtnEight_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "8";
        }

        private void BtnNine_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "9";
        }

        private void BtnZero_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "0";
        }

        private void BtnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text.Length == 0)
                return;

            TxtInputValue.Text = TxtInputValue.Text.Remove(TxtInputValue.Text.Length - 1);
        }

        private void BtnGo_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
