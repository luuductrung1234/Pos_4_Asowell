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
        private List<OrderNoteDetails> ChairOrderDetails { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
