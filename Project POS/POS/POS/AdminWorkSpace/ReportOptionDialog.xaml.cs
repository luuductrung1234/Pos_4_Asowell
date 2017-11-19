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
using System.Windows.Shapes;
using POS.Helper.PrintHelper.Report;
using POS.Repository.DAL;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for ReportOptionDialog.xaml
    /// </summary>
    public partial class ReportOptionDialog : Window
    {
        private DateTime time = DateTime.Now;
        private IListPdfReport _reportHelper;
        private AdminwsOfAsowell _unitofwork;
        private string folderPath = AppPath.ApplicationPath + "\\SerializedData";

        public ReportOptionDialog(IListPdfReport reportHelper, AdminwsOfAsowell unitofwork)
        {
            InitializeComponent();

            _unitofwork = unitofwork;
            _reportHelper = reportHelper;
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (time != null && _reportHelper != null)
                {
                    // generate report
                    _reportHelper.CreatePdfReport(_unitofwork, time, folderPath);
                    

                    MessageBox.Show("new report was generated, please check your folder (path):\n\n" + folderPath);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Generate new report fail! Something went wrong.");
            }
        }

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker picker = sender as DatePicker;
            time = picker.SelectedDate.Value;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    folderPath = dialog.SelectedPath;
                }
            }
        }
    }
}
