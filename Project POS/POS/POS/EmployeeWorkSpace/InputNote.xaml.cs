using System;
using System.Windows;

namespace POS.EmployeeWorkSpace
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
