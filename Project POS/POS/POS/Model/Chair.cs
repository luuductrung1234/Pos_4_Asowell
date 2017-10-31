using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Model
{
    [Serializable]
    public class Chair : INotifyPropertyChanged
    {
        private int _ChairNumber { get; set; }
        private int _TableOfChair { get; set; }
        private List<OrderNoteDetails> _ChairOrderDetails { get; set; }

        public int ChairNumber
        {
            get
            {
                return _ChairNumber;
            }
            set
            {
                _ChairNumber = value;
                OnPropertyChanged("ChairNumber");
            }
        }

        public int TableOfChair
        {
            get
            {
                return _TableOfChair;
            }
            set
            {
                _TableOfChair = value;
            }
        }

        public List<OrderNoteDetails> ChairOrderDetails
        {
            get
            {
                return _ChairOrderDetails;
            }
            set
            {
                _ChairOrderDetails = value;
                OnPropertyChanged("ChairOrderDetails");
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ChairData
    {
        private static List<Chair> _chairList = new List<Chair>
        {
            
        };

        public static List<Chair> ChairList
        {
            get
            {
                return _chairList;
            }
            set
            {
                _chairList = value;
            }
        }
    }
}
