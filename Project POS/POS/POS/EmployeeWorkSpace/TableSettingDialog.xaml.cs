using System;
using System.Windows;
using POS.Model;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for TableSettingDialog.xaml
    /// </summary>
    public partial class TableSettingDialog : Window
    {
        Model.Table curTable = new Model.Table();

        public TableSettingDialog(Model.Table table)
        {
            InitializeComponent();

            curTable = table;

            this.Loaded += TableSettingDialog_Loaded;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void TableSettingDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = Convert.ToInt32(curTable.Position.X);
            this.Top = Convert.ToInt32(curTable.Position.Y);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            foreach(Model.Table t in TableTempData.TbList)
            {
                if(t.TableNumber == curTable.TableNumber)
                {
                    t.ChairAmount = int.Parse(txtChairAmount.Text);

                    ReadWriteData.writeOnUpdateChair(t);
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
