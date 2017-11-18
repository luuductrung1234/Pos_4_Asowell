using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Shapes;
using POS.Entities;

namespace POS.BusinessModel
{
    [Serializable]
    public class Table : INotifyPropertyChanged
    {
        [NonSerialized] private Rectangle _visualTable;
        private int _TableNumber { get; set; }
        private int _ChairAmount { get; set; }
        private List<Chair> _ChairData { get; set; }
        private Point _Position { get; set; }
        private bool _IsPinned { get; set; }
        private OrderNote _TableOrder { get; set; }
        private List<OrderNoteDetail> _TableOrderDetails { get; set; }

        public Boolean IsOrdered { get; set; }

        public Rectangle VisualTable
        {
            get { return _visualTable; }
            set { _visualTable = value; }
        }

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
        public List<Chair> ChairData
        {
            get { return _ChairData; }
            set
            {
                _ChairData = value;
                OnPropertyChanged("ChairData");
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
        public List<OrderNoteDetail> TableOrderDetails
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
        private static List<Table> _tbList = new List<Table>();
//                {
//                    new Table {TableNumber = 1, ChairAmount = 5, Position = new Point(100, 100), IsPinned = true,
//                        ChairData = new List<Chair> { new Chair { ChairNumber = 1, ChairOrderDetails = new List<OrderNoteDetails>() },
//                                                      new Chair { ChairNumber = 2, ChairOrderDetails = new List<OrderNoteDetails>() }, },
//                                    TableOrder = new OrderNote { OrdernoteId = "ORN0000001",
//                                                                    CusId = "CUS0000001",
//                                                                    EmpId = "EMP0000001",
//                                                                    Ordertable = 1,
//                                                                    Ordertime = DateTime.Now},
//                                    TableOrderDetails = new List<OrderNoteDetails>() {
//                                                                new OrderNoteDetails{ProductId="P000000001", OrdernoteId="ORN0000001", Quan=1, Note="", SelectedStats="Beverage" } ,
//                                                                new OrderNoteDetails{ProductId="P000000004", OrdernoteId="ORN0000001", Quan=4, Note="", SelectedStats="Beverage" }
//},
//                    },
//                    new Table {TableNumber = 2, ChairAmount = 10, Position = new Point(500, 500), IsPinned = true,
//                                    TableOrder = new OrderNote { OrdernoteId = "ORN0000002",
//                                                                    CusId = "CUS0000002",
//                                                                    EmpId = "EMP0000001",
//                                                                    Ordertable = 2,
//                                                                    Ordertime = DateTime.Now},
//                                    TableOrderDetails = new List<OrderNoteDetails>()},
//                    new Table {TableNumber = 3, ChairAmount = 20, Position = new Point(100, 500), IsPinned = true,
//                                    TableOrder = new OrderNote { OrdernoteId = "ORN0000003",
//                                                                    CusId = "CUS0000003",
//                                                                    EmpId = "EMP0000001",
//                                                                    Ordertable = 3,
//                                                                    Ordertime = DateTime.Now},
//                                    TableOrderDetails = new List<OrderNoteDetails>()},
//                    new Table {TableNumber = 4, ChairAmount = 4, Position = new Point(500, 100), IsPinned = true,
//                                    TableOrder = new OrderNote { OrdernoteId = "ORN0000004",
//                                                                    CusId = "CUS0000004",
//                                                                    EmpId = "EMP0000001",
//                                                                    Ordertable = 4,
//                                                                    Ordertime = DateTime.Now},
//                                    TableOrderDetails = new List<OrderNoteDetails>()},
//                };

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
