using System.Windows;
using POS.BusinessModel;

namespace POS
{
    /// <summary>
    /// Interaction logic for DatabaseConfigWindow.xaml
    /// </summary>
    public partial class DatabaseConfigWindow : Window
    {
        public DatabaseConfigWindow()
        {
            InitializeComponent();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            string initialCatalog = txtInitialCatalog.Text.Trim();
            string source = txtDataSource.Text.Trim();
            string userId = txtUserId.Text.Trim();
            string pass = txtPassword.Password.Trim();

            if (initialCatalog.Length == 0 || source.Length == 0 || userId.Length == 0 || pass.Length == 0)
            {
                MessageBox.Show("Some input field is not correct! Please check!");
                return;
            }

            //App.Current.Properties["InitialCatalog"] = initialCatalog;
            //App.Current.Properties["Source"] = source;
            //App.Current.Properties["UserId"] = userId;
            //App.Current.Properties["Password"] = pass;
            //App.Current.Properties["IsConfigDB"] = "true";

            ReadWriteData.WriteDBConfig(initialCatalog + "," + source + "," + userId + "," + pass + "," + "true");

            MessageBox.Show("Please reset the Application!");
            Close();
        }
    }
}
