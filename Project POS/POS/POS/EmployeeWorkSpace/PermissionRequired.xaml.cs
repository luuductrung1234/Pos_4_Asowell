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
        private EmployeewsOfCloudAsowell _cloudAsowellUnitofwork;
        MaterialDesignThemes.Wpf.Chip _cUser;

        public PermissionRequired(EmployeewsOfCloudAsowell cloudAsowellUnitofwork, MaterialDesignThemes.Wpf.Chip cUser)
        {
            _cloudAsowellUnitofwork = cloudAsowellUnitofwork;
            _cUser = cUser;
            InitializeComponent();

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
                    List<AdminRe> AdList = _cloudAsowellUnitofwork.AdminreRepository.Get().ToList();

                    //Get Admin
                    bool isFoundAd = false;
                    foreach (var item in AdList)
                    {
                        if (item.Username.Equals(username) && item.Pass.Equals(pass))
                        {
                            App.Current.Properties["AdLogin"] = item;

                            isFoundAd = true;
                            break;
                        }

                        if (!isFoundAd)
                        {
                            MessageBox.Show("incorrect username or password");
                            return;
                        }
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

        private void btnAcceptCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
