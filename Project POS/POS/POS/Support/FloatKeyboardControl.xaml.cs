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
        public string InputValue { get; set; }
        private RoutedEventHandler _goClick;
        public event RoutedEventHandler GoClick
        {
            add { _goClick += value; }
            remove
            {
                _goClick -= value;
            }
        }


        public FloatKeyboardControl()
        {
            InitializeComponent();
        }


        private void TxtInputValue_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            InputValue = TxtInputValue.Text;
        }

        private void BtnDeleteInput_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text = "";
            //InputValue = "";
        }

        private void BtnCharacterKey_Click(object sender, RoutedEventArgs e)
        {
            Button clickButton = sender as Button;
            TxtInputValue.Text += clickButton.Content.ToString();
            //InputValue += clickButton.Content.ToString();
        }

        private void BtnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text.Length == 0)
                return;

            TxtInputValue.Text = TxtInputValue.Text.Remove(TxtInputValue.Text.Length - 1);
            //InputValue = InputValue.Remove(InputValue.Length - 1);
        }



        private void BtnGo_OnClick(object sender, RoutedEventArgs e)
        {
            _goClick(sender, e);

            TxtInputValue.Text = "";
        }

        private void BtnShift_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
