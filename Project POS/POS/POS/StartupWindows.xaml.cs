using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace POS
{
    /// <summary>
    /// Interaction logic for StartupWindows.xaml
    /// </summary>
    public partial class StartupWindows : Window
    {
        public StartupWindows()
        {
            InitializeComponent();

            Task.Run(() =>
            {
                Thread.Sleep(4000);
                Dispatcher.Invoke(() =>
                {

                    
                    Login login = new Login();
                    login.Show();
                    this.Close();
                });

            });
        }
    }
}
