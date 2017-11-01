using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using POS.Model;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public POS.Model.Table currentTable { get; set; }
        public Model.Chair currentChair { get; set; }
        internal Table b = new Table();
        internal Dash d = new Dash();
        internal Entry en = new Entry();
        internal Info info = new Info();
        internal Login login = new Login();

        public MainWindow()
        {
            InitializeComponent();
            currentTable = null;
            Employee emp = App.Current.Properties["EmpLogin"] as Employee;

            cUser.Content= emp.Username;



        }

        private void bntDash_Click(object sender, RoutedEventArgs e)
        {
            bntTable.IsEnabled = true;
            bntDash.IsEnabled = false;
            bntEntry.IsEnabled = true;
            bntInfo.IsEnabled = true;
            myFrame.Navigate(d);

        }
        
        private void bntTable_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(b); 
            bntTable.IsEnabled = false;
            bntDash.IsEnabled = true;
            bntEntry.IsEnabled = true;
            bntInfo.IsEnabled = true;
        }

        private void bntEntry_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(en);
            bntTable.IsEnabled = true;
            bntDash.IsEnabled = true;
            bntEntry.IsEnabled = false;
            bntInfo.IsEnabled = true;
        }

        private void bntInfo_Click(object sender, RoutedEventArgs e)
        {
            myFrame.Navigate(info);
            bntTable.IsEnabled = true;
            bntDash.IsEnabled = true;
            bntEntry.IsEnabled = true;
            bntInfo.IsEnabled = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            currentTable = TableTempData.TbList.First();
            this.en.ucOrder.RefreshControl();
        }

        private void DemoItemsListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void ListBoxItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmployeeDetail emd = new EmployeeDetail(cUser.Content.ToString());
            emd.ShowDialog();
        }

        private void bntLogout_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
            login.Show();
            
        }
    }
}
