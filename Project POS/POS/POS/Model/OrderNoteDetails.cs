using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    [Serializable]
    public class OrderNoteDetails : INotifyPropertyChanged
    {
        private string _ordernote_id;
        private string _product_id;
        private string _stats;
        private int _quan;
        private string _note;
        private ObservableCollection<string> _statusItems = new ObservableCollection<string>{"Stater", "Main Cost", "Dessert", "Drink"};

        public string Ordernote_id
        {
            get
            {
                return _ordernote_id;
            }
            set
            {
                _ordernote_id = value;
                OnPropertyChanged("Ordernote_id");
            }
        }
        public string Product_id
        {
            get
            {
                return _product_id;
            }
            set
            {
                _product_id = value;
                OnPropertyChanged("Product_id");
            }
        }
        public ObservableCollection<string> StatusItems
        {
            get
            {
                return _statusItems;
            }
            set
            {
                _statusItems = value; OnPropertyChanged("SalesPeriods");
            }
        }
        public string SelectedStats
        {
            get { return _stats; }
            set
            {
                _stats = value;
                OnPropertyChanged("SelectedStats");
                System.Windows.MessageBox.Show("New SelectedStats setting");
            }
        }
        public int Quan
        {
            get
            {
                return _quan;
            }
            set
            {
                _quan = value;
                OnPropertyChanged("Quan");
                System.Windows.MessageBox.Show("New Quan setting");
            }
        }
        public string Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
                OnPropertyChanged("Note");
                System.Windows.MessageBox.Show("New Note setting");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
