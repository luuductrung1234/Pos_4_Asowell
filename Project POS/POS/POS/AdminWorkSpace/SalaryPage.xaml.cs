using POS.Entities;
using POS.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using POS.Helper.PrintHelper.Report;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for SalaryPage.xaml
    /// </summary>
    public partial class SalaryPage : Page
    {
        AdminwsOfCloudPOS _unitofwork;
        IEnumerable<SalaryNote> SalList;
        IEnumerable<WorkingHistory> WhList;
        public SalaryPage(AdminwsOfCloudPOS unitofwork)
        {
            InitializeComponent();
            _unitofwork = unitofwork;
            SalList = unitofwork.SalaryNoteRepository.Get(includeProperties: "Employee,WorkingHistories");
            WhList = unitofwork.WorkingHistoryRepository.Get(includeProperties: "Employee");
            lvSalary.ItemsSource = SalList;
            lvWokingHistory.ItemsSource = WhList;
            initMonthYear();
        }

        private void initMonthYear()
        {
            int checkYear = 0;
            cboYear.Items.Clear();
            int year = DateTime.Now.Year;
            cboYear.Items.Add("--");
            cboYear.Items.Add(year);

            var SalList = _unitofwork.SalaryNoteRepository.Get();
            foreach (var s in SalList)
            {
                if (s.ForYear != year)
                {
                    if (checkYear != s.ForYear)
                    {
                        cboYear.Items.Add(s.ForYear);
                        checkYear = s.ForYear;
                    }
                }
            }

            cboMonth.Items.Clear();
            cboMonth.Items.Add("--");
            for (int i = 0; i < 12; i++)
            {
                cboMonth.Items.Add(i + 1);
            }

            cboYear.SelectedIndex = 0;
            cboMonth.SelectedIndex = 0;
            cboYear.SelectionChanged += cboYear_SelectionChanged;
            cboMonth.SelectionChanged += cboMonth_SelectionChanged;
        }

        private void lvData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SalaryNote sln = lvSalary.SelectedItem as SalaryNote;
            lvWokingHistory.ItemsSource = WhList.Where(c => c.ResultSalary.Equals(sln.SnId));
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            string filter = SearchBox.Text.Trim();

            if (filter.Length != 0)
            {
                SalList = SalList.Where(x => Regex.IsMatch(x.Employee.Name, filter, RegexOptions.IgnoreCase));
                lvSalary.ItemsSource = SalList;
            }
            else
            {
                SalList = _unitofwork.SalaryNoteRepository.Get(includeProperties: "Employee,WorkingHistories");
                lvSalary.ItemsSource = SalList;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchBox.Text.Trim();

            if(filter.Length != 0)
            {
                SalList = SalList.Where(x => x.Employee.Name.Contains(filter));
                lvSalary.ItemsSource = SalList;
            }
            else
            {
                SalList = _unitofwork.SalaryNoteRepository.Get(includeProperties: "Employee,WorkingHistories");
                lvSalary.ItemsSource = SalList;
            }
        }

        private void cboMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string filter = SearchBox.Text.Trim();
            if (filter.Length != 0)
            {
                SalList = SalList.Where(x => x.Employee.Name.Contains(filter));
            }

            ComboBox cboM = sender as ComboBox;

            int month = 0;
            int year = 0;

            try
            {
                month = (int)(cboM.SelectedValue);
                year = (int)(cboYear.SelectedValue);
            }
            catch (Exception ex)
            {
                if (cboMonth.SelectedValue.Equals("--"))
                {
                    if (cboYear.SelectedValue.Equals("--"))
                    {
                        lvSalary.ItemsSource = SalList;
                        return;
                    }

                    lvSalary.ItemsSource = SalList.Where(x => x.ForYear.Equals((int)cboYear.SelectedValue));
                    return;
                }

                if (cboYear.SelectedValue.Equals("--"))
                {
                    lvSalary.ItemsSource = SalList.Where(x => x.ForMonth.Equals((int)cboM.SelectedValue));
                    return;
                }
            }

            lvSalary.ItemsSource = SalList.Where(x => x.ForMonth.Equals((int)cboM.SelectedValue) && x.ForYear.Equals((int)cboYear.SelectedValue));
        }

        private void cboYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string filter = SearchBox.Text.Trim();
            if (filter.Length != 0)
            {
                SalList = SalList.Where(x => x.Employee.Name.Contains(filter));
            }

            ComboBox cboY = sender as ComboBox;

            int month = 0;
            int year = 0;

            try
            {
                year = (int)(cboY.SelectedValue);
                month = (int)(cboMonth.SelectedValue);
            }
            catch (Exception ex)
            {
                if (cboY.SelectedValue.Equals("--"))
                {
                    if (cboMonth.SelectedValue.Equals("--"))
                    {
                        lvSalary.ItemsSource = SalList;
                        return;
                    }

                    lvSalary.ItemsSource = SalList.Where(x => x.ForMonth.Equals((int)cboMonth.SelectedValue));
                    return;
                }

                if (cboMonth.SelectedValue.Equals("--"))
                {
                    lvSalary.ItemsSource = SalList.Where(x => x.ForYear.Equals((int)cboY.SelectedValue));
                    return;
                }
            }

            lvSalary.ItemsSource = SalList.Where(x => x.ForMonth.Equals((int)cboMonth.SelectedValue) && x.ForYear.Equals((int)cboY.SelectedValue));
        }

        //public string ConvertFrom(string str)
        //{
        //    byte[] utf8Bytes = Encoding.UTF8.GetBytes(str);
        //    str = Encoding.UTF8.GetString(utf8Bytes);
        //    return str;
        //}

        private string DecodeFromUtf8(string utf8String)
        {
            // read the string as UTF-8 bytes.
            byte[] encodedBytes = Encoding.UTF8.GetBytes(utf8String);

            // convert them into unicode bytes.
            byte[] unicodeBytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encodedBytes);

            // builds the converted string.
            return Encoding.Unicode.GetString(encodedBytes);
        }

        private void BtnOverViewReport_OnClick(object sender, RoutedEventArgs e)
        {
            var optionDialog = new ReportSalaryOptionDialog(new SalaryNoteReport(), _unitofwork);
            optionDialog.Show();
        }
    }
}
