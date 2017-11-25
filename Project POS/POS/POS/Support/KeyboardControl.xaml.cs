using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using POS.EmployeeWorkSpace;
using POS.Entities;
using POS.Entities.CustomEntities;
using POS.Repository.DAL;

namespace POS.Support
{
    /// <summary>
    /// Interaction logic for KeyboardControl.xaml
    /// </summary>
    public partial class KeyboardControl : UserControl
    {
        public string InputValue { get; set; }
        public RoutedEventHandler _goClick;
        public event RoutedEventHandler GoClick
        {
            add { _goClick += value; }
            remove
            {
                _goClick -= value;
            }
        }


        public KeyboardControl()
        {
            InitializeComponent();
        }

        private void BtnDeleteInput_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text = "";
            InputValue = "";
        }

        private void BtnOne_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "1";
            InputValue += "1";
        }

        private void BtnTwo_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "2";
            InputValue += "2";
        }

        private void BtnThree_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "3";
            InputValue += "3";
        }

        private void BtnFour_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "4";
            InputValue += "4";
        }

        private void BtnFive_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "5";
            InputValue += "5";
        }

        private void BtnSix_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "6";
            InputValue += "6";
        }

        private void BtnSeven_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "7";
            InputValue += "7";
        }

        private void BtnEight_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "8";
            InputValue += "8";
        }

        private void BtnNine_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "9";
            InputValue += "9";
        }

        private void BtnZero_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text += "0";
            InputValue += "0";
        }

        private void BtnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Text.Length == 0 || InputValue.Length == 0)
                return;

            TxtInputValue.Text = TxtInputValue.Text.Remove(TxtInputValue.Text.Length - 1);
            InputValue = InputValue.Remove(InputValue.Length - 1);
        }

        private async void BtnGo_OnClick(object sender, RoutedEventArgs e)
        {
            _goClick(sender, e);
        }

        public void ButtonGoAbleState(bool state)
        {
            BtnGo.IsEnabled = state;
        }
    }
}
