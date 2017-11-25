using POS.BusinessModel;
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

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for SettingTableSize.xaml
    /// </summary>
    public partial class SettingTableSize : Page
    {
        public SettingTableSize()
        {
            InitializeComponent();

            this.Loaded += SettingTableSize_Loaded;
        }

        private void SettingTableSize_Loaded(object sender, RoutedEventArgs e)
        {
            txtWidth.Text = ReadWriteData.readTableSize()[0];
            txtHeight.Text = ReadWriteData.readTableSize()[1];
        }

        private void CheckNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                e.Handled = !Char.IsNumber(e.Text[0]);
            }
        }

        private void txtWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtWidth.Text.Trim().Length <= 1)
            {
                return;
            }

            int width = Convert.ToInt32(txtWidth.Text.Trim());
            if(width > 150)
            {
                txtWidth.Text = "150";
            }
            if(width < 10)
            {
                txtWidth.Text = "10";
            }

            width = Convert.ToInt32(txtWidth.Text);

            recDemo.Width = width;
        }

        private void txtHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtHeight.Text.Trim().Length <= 1)
            {
                return;
            }

            int height = Convert.ToInt32(txtHeight.Text.Trim());
            if(height > 150)
            {
                txtHeight.Text = "150";
            }
            if(height < 10)
            {
                txtHeight.Text = "10";
            }

            height = Convert.ToInt32(txtHeight.Text.Trim());

            recDemo.Height = height;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if(recDemo.Width < 10 || recDemo.Height < 10)
            {
                MessageBox.Show("Width and Height must be greater than 10 and lesser than 150!");
                return;
            }

            ReadWriteData.writeTableSize(recDemo.Width + "-" + recDemo.Height);

            MessageBoxResult messRe = MessageBox.Show("You must be logout and login again for take effect about this change! Logout now?", "Warning!", MessageBoxButton.YesNo);
            if(messRe == MessageBoxResult.Yes)
            {
                ((MainWindow)Window.GetWindow(this)).Close();
                Login log = new Login();
                log.Show();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtWidth.Text = ReadWriteData.readTableSize()[0];
            txtHeight.Text = ReadWriteData.readTableSize()[1];
        }

        private void BtnPrinterApply_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
