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
using MaterialDesignThemes.Wpf;

namespace POS.Support
{
    /// <summary>
    /// Interaction logic for PassKeyboardControl.xaml
    /// </summary>
    public partial class PassKeyboardControl : UserControl
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

        private RoutedEventHandler _turnOffKeyboard;
        public event RoutedEventHandler TurnOffKeyboard
        {
            add { _turnOffKeyboard += value; }
            remove
            {
                _turnOffKeyboard -= value;
            }
        }



        public PassKeyboardControl()
        {
            InitializeComponent();
        }

        private void TxtInputValue_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            InputValue = TxtInputValue.Password;
            picBackspace.Kind = PackIconKind.Backspace;
        }

        private void BtnDeleteInput_Click(object sender, RoutedEventArgs e)
        {
            TxtInputValue.Password = "";
            picBackspace.Kind = PackIconKind.KeyboardReturn;
        }

        private void ButtonKey_Click(object sender, RoutedEventArgs e)
        {
            Button clickButton = sender as Button;
            TxtInputValue.Password += clickButton.Content.ToString();
            //InputValue += clickButton.Content.ToString();
        }

        private void BtnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInputValue.Password.Length == 0)
            {
                _turnOffKeyboard(sender, e);
                return;
            }
                

            TxtInputValue.Password = TxtInputValue.Password.Remove(TxtInputValue.Password.Length - 1);
            //InputValue = InputValue.Remove(InputValue.Length - 1);

            if (TxtInputValue.Password.Length == 0)
            {
                picBackspace.Kind = PackIconKind.KeyboardReturn;
            }
        }




        private async void BtnGo_OnClick(object sender, RoutedEventArgs e)
        {
            _goClick(sender, e);

            TxtInputValue.Password = "";
            picBackspace.Kind = PackIconKind.KeyboardReturn;
        }

        public void ButtonGoAbleState(bool state)
        {
            BtnGo.IsEnabled = state;
        }
        
    }
}
