using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    public class OrderNoteDetails : INotifyPropertyChanged
    {
        private string _ordernote_id;
        private string _product_id;
        private string _stats;
        private int _quan;
        private string _note;

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
        public string Stats
        {
            get
            {
                return _stats;
            }
            set
            {
                _stats = value;
                OnPropertyChanged("Stats");
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
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class OrderDetailsData
    {
        public static ObservableCollection<OrderNoteDetails> OrderDetailslist = new ObservableCollection<OrderNoteDetails>();

    }
}
