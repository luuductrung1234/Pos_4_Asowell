using POS.Entities;
using POS.Repository.DAL;
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

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for PermissionRequired.xaml
    /// </summary>
    public partial class PermissionRequired : Window
    {
        private EmployeewsOfCloudPOS _cloudPosUnitofwork;
        MaterialDesignThemes.Wpf.Chip _cUser;

        public PermissionRequired(EmployeewsOfCloudPOS cloudPosUnitofwork, MaterialDesignThemes.Wpf.Chip cUser)
        {
            _cloudPosUnitofwork = cloudPosUnitofwork;
            _cUser = cUser;
            InitializeComponent();

            txtUsername.Focus();

            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private async void btnAcceptLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string pass = txtPass.Password.Trim();
            try
            {
                btnAcceptLogin.IsEnabled = false;
                PgbLoginProcess.Visibility = Visibility.Visible;
                await Async(username, pass, null);

                btnAcceptLogin.IsEnabled = true;
                PgbLoginProcess.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task Async(string username, string pass, object p)
        {
            try
            {
                await Task.Run(() =>
                {
                    List<AdminRe> AdList = _cloudPosUnitofwork.AdminreRepository.Get().ToList();

                    var ad = AdList.FirstOrDefault(x => x.Username.Equals(username) && x.DecryptedPass.Equals(pass));
                    //Get Admin
                    bool isFoundAd = false;
                    if (ad != null)
                    {
                        App.Current.Properties["AdLogin"] = ad;
                        isFoundAd = true;
                    }

                    if (!isFoundAd)
                    {
                        MessageBox.Show("incorrect username or password");
                        return;
                    }

                    Dispatcher.Invoke(() =>
                    {
                        _cUser.Content = (App.Current.Properties["AdLogin"] as AdminRe).Username;
                        this.Close();
                    });

                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPass.Focus();
            }
        }

        private async void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string username = txtUsername.Text.Trim();
                string pass = txtPass.Password.Trim();
                try
                {
                    btnAcceptLogin.IsEnabled = false;
                    PgbLoginProcess.Visibility = Visibility.Visible;
                    await Async(username, pass, null);

                    btnAcceptLogin.IsEnabled = true;
                    PgbLoginProcess.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnAcceptCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
