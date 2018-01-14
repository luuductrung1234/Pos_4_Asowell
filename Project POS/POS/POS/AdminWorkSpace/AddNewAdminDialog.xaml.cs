using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using POS.Entities;
using POS.Repository.DAL;

namespace POS.AdminWorkSpace
{
    /// <summary>
    /// Interaction logic for AddNewAdminDialog.xaml
    /// </summary>
    public partial class AddNewAdminDialog : Window
    {
        private AdminwsOfCloudPOS _unitofwork;
        private AdminRe _admin;



        public AddNewAdminDialog(AdminwsOfCloudPOS unitofwork)
        {
            InitializeComponent();

            _unitofwork = unitofwork;
            _admin = new AdminRe();
            initControlAdd();
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void initControlAdd()
        {
            List<dynamic> roleList = new List<dynamic>
            {
                new { role = 1, roleDisplay = "Software Admin"},
                new { role = 2, roleDisplay = "Asowel Admin"},
                new { role = 3, roleDisplay = "AdPress Admin"},
                new { role = 4, roleDisplay = "Higher Admin"}
            };
            cboRole.ItemsSource = roleList;
            cboRole.SelectedValuePath = "role";
            cboRole.DisplayMemberPath = "roleDisplay";
        }




        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string pass = txtPass.Password.Trim();

                //check username
                if (username.Length == 0 || username.Length > 50)
                {
                    MessageBox.Show("Username is not valid!");
                    txtUsername.Focus();
                    return;
                }

                var newemp = _unitofwork.EmployeeRepository.Get(x => x.Username.Equals(username)).ToList();

                if (newemp.Count != 0)
                {
                    MessageBox.Show("Username is already exist! Please try again!");
                    txtUsername.Focus();
                    return;
                }

                //check pass
                if (pass.Length == 0 || pass.Length > 50)
                {
                    MessageBox.Show("Password is not valid!");
                    txtPass.Focus();
                    return;
                }

                string passcon = txtCon.Password.Trim();
                if (!passcon.Equals(pass))
                {
                    MessageBox.Show("Confirm password is not match!");
                    txtCon.Focus();
                    return;
                }


                //check name
                string name = txtName.Text.Trim();
                if (name.Length == 0 || name.Length > 50)
                {
                    MessageBox.Show("Name is not valid!");
                    txtName.Focus();
                    return;
                }

                //check role
                int role = 0;

                if (cboRole.SelectedValue == null)
                {
                    MessageBox.Show("Role must be selected!");
                    return;
                }

                role = (int)cboRole.SelectedValue;

                if (role == 0)
                {
                    MessageBox.Show("Role must be selected!");
                    return;
                }


                // Adding
                

                AdminRe newAd = new AdminRe()
                {
                    AdId = "",
                    Username = username,
                    Pass = pass,
                    Name = name,
                    AdRole = role
                };

                _unitofwork.AdminreRepository.Insert(newAd);
                _unitofwork.Save();

                MessageBox.Show("Insert " + newAd.Name + "(" + newAd.AdId + ") successful!");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong. Can not add or update admin info. Please check the details again!");
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
