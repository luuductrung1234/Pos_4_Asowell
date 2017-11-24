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

namespace POS.WareHouseWorkSpace
{
    /// <summary>
    /// Interaction logic for InputNote.xaml
    /// </summary>
    public partial class InputNote : Window
    {
        public InputNote(string defautNote)
        {
            InitializeComponent();
            txtNote.Text = defautNote;
        }
        public string Note
        {
            get { return txtNote.Text; }
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtNote.SelectAll();
            txtNote.Focus();
        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
