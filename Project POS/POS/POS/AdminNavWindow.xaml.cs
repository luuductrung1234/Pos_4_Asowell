﻿using System;
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
using log4net;
using POS.AdminWorkSpace;
using POS.WareHouseWorkSpace;

namespace POS
{
    /// <summary>
    /// Interaction logic for AdminNavWindow.xaml
    /// </summary>
    public partial class AdminNavWindow : Window
    {
        private ILog AppLog;

        public AdminNavWindow(ILog AppLog)
        {
            this.AppLog = AppLog;
            InitializeComponent();
        }



        private void GotoWareHouseWSButton_OnClick(object sender, RoutedEventArgs e)
        {
            WareHouseWindow whWindow = new WareHouseWindow(AppLog);
            whWindow.Show();

            this.Close();
        }

        private void GotoAdminWSButton_OnClick(object sender, RoutedEventArgs e)
        {

            AdminWindow adminWindow = new AdminWindow(AppLog);
            adminWindow.Show();

            this.Close();
        }
    }
}
