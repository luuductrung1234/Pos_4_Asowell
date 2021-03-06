﻿using System.Windows;
using POS.AdminWorkSpace;
using POS.Entities;
using POS.WareHouseWorkSpace;
using POS.AdPressWareHouseWorkSpace;

namespace POS
{
    /// <summary>
    /// Interaction logic for AdminNavWindow.xaml
    /// </summary>
    public partial class AdminNavWindow : Window
    {

        public AdminNavWindow()
        {
            InitializeComponent();

            var curAd = App.Current.Properties["AdLogin"]  as AdminRe;


            // Control layout depend on logging Admin
            if (curAd.AdRole == (int) AdminReRole.AsowelAd)
            {
                stpAdpress.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (curAd.AdRole == (int) AdminReRole.AdPressAd)
                {
                    stpAsowel.Visibility = Visibility.Collapsed;
                }
            }
        }



        private void GotoWareHouseWSButton_OnClick(object sender, RoutedEventArgs e)
        {
            WareHouseWindow whWindow = new WareHouseWindow();
            whWindow.Show();

            this.Close();
        }

        private void GotoAdminWSButton_OnClick(object sender, RoutedEventArgs e)
        {

            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();

            this.Close();
        }

        private void GotoAdPressWSButton_OnClick(object sender, RoutedEventArgs e)
        {
            APWareHouseWindow apWindow = new APWareHouseWindow();
            apWindow.Show();

            this.Close();
        }
    }
}
