using System;
using System.Windows;
using POS.Context;
using POS.Entities;
using POS.Repository;
using POS.Repository.DAL;
using POS.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for EmployeeDetail.xaml
    /// </summary>
    public partial class EmployeeDetail : Window
    {
        private EmployeewsOfCloudPOS _cloudPosUnitofwork;
        Employee em;

        public EmployeeDetail(string UserName,  EmployeewsOfCloudPOS cloudPosUnitofwork)
        {
            _cloudPosUnitofwork = cloudPosUnitofwork;
            InitializeComponent();
            loadData(UserName);
            InitlsWh();
        }

        private void loadData(string UserName)
        {
            em = _cloudPosUnitofwork.EmployeeRepository.Get(e => e.Username.Equals(UserName)).First();

            this.EmployeeInfo.DataContext = em;
        }

        private void InitlsWh()
        {
            ShowWHData.showWHList.Clear();
            var whListAll = _cloudPosUnitofwork.WorkingHistoryRepository.Get(w => w.EmpId.Equals(em.EmpId) && w.StartTime.Month.Equals(DateTime.Now.Month) && w.StartTime.Year.Equals(DateTime.Now.Year)).ToList();
            foreach (var i in whListAll)
            {
                ShowWH newWH = new ShowWH();
                newWH.WorkTime = formatString((i.EndTime - i.StartTime).Hours, (i.EndTime - i.StartTime).Minutes, (i.EndTime - i.StartTime).Seconds);
                newWH.WorkDate = i.StartTime;

                int h = (i.EndTime - i.StartTime).Hours;
                int m = (i.EndTime - i.StartTime).Minutes;
                int s = (i.EndTime - i.StartTime).Seconds;
                
                newWH.TimePercent = (int)((((double)h) + (double)m/60.0 + (double)s/3600.0)/24.0*100);

                ShowWHData.showWHList.Add(newWH);
            }

            lsWH.ItemsSource = ShowWHData.showWHList;
        }

        private string formatString(int hours, int minutes, int seconds)
        {
            string st = "";
            string fH = "", fm = "", fs = "";
            fH = hours + "";
            fm = minutes + "";
            fs = seconds + "";
            if (hours < 10)
            {
                fH = "0" + fH;
            }
            if (minutes < 10)
            {
                fm = "0" + fm;
            }
            if (seconds < 10)
            {
                fs = "0" + fs;
            }
            st = fH + ":" + fm + ":" + fs;
            return st;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeChangePass empPass = new EmployeeChangePass(_cloudPosUnitofwork, em);
            empPass.ShowDialog();
        }
    }

    public class ShowWH
    {
        private int _timePercent;
        private string _workTime;
        public DateTime WorkDate { get; set; }

        public int TimePercent
        {
            get
            {
                return _timePercent;
            }
            set
            {
                _timePercent = value;
            }
        }
        
        public string WorkTime
        {
            get
            {
                return _workTime;
            }
            set
            {
                _workTime = value;
            }
        }
    }

    public class ShowWHData
    {
        public static List<ShowWH> showWHList = new List<ShowWH>();
    }
}
