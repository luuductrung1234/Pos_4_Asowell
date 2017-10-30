using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POS.Model
{
    [Serializable]
    public class Table : INotifyPropertyChanged
    {
        private int _TableNumber { get; set; }
        private int _ChairAmount { get; set; }
        //private List<Chair> _ChairData{ get; set; }
        private Point _Position { get; set; }
        private bool _IsPinned { get; set; }
        private OrderNote _TableOrder { get; set; }
        private List<OrderNoteDetails> _TableOrderDetails { get; set; }

        public int TableNumber
        {
            get { return _TableNumber; }
            set
            {
                _TableNumber = value;
                OnPropertyChanged("TableNumber");
            }
        }
        public int ChairAmount
        {
            get { return _ChairAmount; }
            set
            {
                _ChairAmount = value;
                OnPropertyChanged("ChairAmount");
            }
        }
        public Point Position
        {
            get { return _Position; }
            set
            {
                _Position = value;
                OnPropertyChanged("Position");
            }
        }
        public bool IsPinned
        {
            get { return _IsPinned; }
            set { _IsPinned = value; }
        }
        public OrderNote TableOrder
        {
            get { return _TableOrder; }
            set
            {
                _TableOrder = value;
                OnPropertyChanged("TableOrder");
            }
        }
        public List<OrderNoteDetails> TableOrderDetails
        {
            get { return _TableOrderDetails; }
            set
            {
                _TableOrderDetails = value;
                OnPropertyChanged("TableOrderDetails");
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    // temporary data
    public class TableTempData
    {
        private static List<Table> _tbList = new List<Table>()
                {
                    new Table {TableNumber = 1, ChairAmount = 5, Position = new Point(100, 100), IsPinned = true,
                                    TableOrder = new OrderNote { ordernote_id = "ORN0000001",
                                                                    cus_id = "CUS0000001",
                                                                    emp_id = "EMP0000001",
                                                                    ordertable = 1,
                                                                    ordertime = DateTime.Now},
                                    TableOrderDetails = new List<OrderNoteDetails>() {
                                                                new OrderNoteDetails{Product_id="P000000001", Ordernote_id="ORN0000001", Quan=1, Note="", SelectedStats="Drink" } ,
                                                                new OrderNoteDetails{Product_id="P000000004", Ordernote_id="ORN0000001", Quan=4, Note="", SelectedStats="Drink" }
},
                    },
                    new Table {TableNumber = 2, ChairAmount = 10, Position = new Point(500, 500), IsPinned = true,
                                    TableOrder = new OrderNote { ordernote_id = "ORN0000002",
                                                                    cus_id = "CUS0000002",
                                                                    emp_id = "EMP0000001",
                                                                    ordertable = 2,
                                                                    ordertime = DateTime.Now},
                                    TableOrderDetails = new List<OrderNoteDetails>()},
                    new Table {TableNumber = 3, ChairAmount = 20, Position = new Point(100, 500), IsPinned = true,
                                    TableOrder = new OrderNote { ordernote_id = "ORN0000003",
                                                                    cus_id = "CUS0000003",
                                                                    emp_id = "EMP0000001",
                                                                    ordertable = 3,
                                                                    ordertime = DateTime.Now},
                                    TableOrderDetails = new List<OrderNoteDetails>()},
                    new Table {TableNumber = 4, ChairAmount = 4, Position = new Point(500, 100), IsPinned = true,
                                    TableOrder = new OrderNote { ordernote_id = "ORN0000004",
                                                                    cus_id = "CUS0000004",
                                                                    emp_id = "EMP0000001",
                                                                    ordertable = 4,
                                                                    ordertime = DateTime.Now},
                                    TableOrderDetails = new List<OrderNoteDetails>()},
                };

        public static List<Table> TbList
        {
            get
            {
                return _tbList;
            }
            set
            {
                _tbList = value;
            }
        }
    }

}
