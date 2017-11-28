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

        private void TxtInputValue_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            InputValue = TxtInputValue.Text;
        }

        private void BtnDeleteInput_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Text = "";
            //InputValue = "";
        }

        private void ButtonKey_Click(object sender, RoutedEventArgs e)
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
