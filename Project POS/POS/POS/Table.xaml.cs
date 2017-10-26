using Microsoft.Win32;
using POS.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POS
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : Page
    {
        public Table()
        {
            InitializeComponent();

            initTable();
        }

        //load table data tu file "table.txt"
        private void initTable()
        {
            initBackgroundTable(readTableData(0, "tableImagePath.txt"));

            //readTableData(1, "tableRuntimeHistory.txt");
        }

        string allCurrentTableData;
        //lay thong tin table image, tat ca table hien co
        private string readTableData(int line, string fileName) //0: tableImagePath; 1: tableRuntimeHistory
        {
            try
            {
                if (line == 0)
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    {
                        using (StreamReader rd = new StreamReader(fs, Encoding.UTF8))
                        {
                            string tableImagePath = rd.ReadLine();
                            return tableImagePath;
                        }
                    }
                }

                if (line == 1)
                {
                    using (Stream stream = File.Open(fileName, FileMode.Open))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                        List<Table> currentTableList = (List<Table>)bformatter.Deserialize(stream);
                        Rectangle rec;
                        Thickness m;

                        foreach (var ctl in currentTableList)
                        {

                            maxTableCurrentNumber = 0;

                            buttonTableCurrentNumber++;

                            rec = new Rectangle();
                            if (maxTableCurrentNumber < 10)
                            {
                                rec.Name = "0";
                            }
                            else
                            {
                                rec.Name = "0";
                            }

                            rec.HorizontalAlignment = HorizontalAlignment.Left;
                            rec.VerticalAlignment = VerticalAlignment.Top;
                            m = rec.Margin;
                            m.Left = Convert.ToInt32(int.Parse("100"));
                            m.Top = Convert.ToInt32(int.Parse("100"));
                            rec.Margin = m;
                            rec.Width = 120;
                            rec.Height = 60;
                            rec.Fill = Brushes.Red;
                            rec.Opacity = 0.65;
                            rec.ToolTip = rec.Name;

                            rec.MouseLeftButtonDown += btnTableAdded_StartDrag;
                            rec.MouseMove += btnTableAdded_MoveDrag;
                            rec.MouseLeftButtonDown += btnTableAdded_Click;
                            rec.MouseRightButtonDown += btnTableAdded_ContextMenu;

                            rec.Cursor = Cursors.SizeAll;

                            Panel.SetZIndex(rec, 30);
                            grTable.Children.Add(rec);

                            imgTable.MouseMove -= crossCursorToAdd;
                            imgTable.MouseLeftButtonDown -= changeToNormalCursor;
                            iii = 0;

                            //return rd.ReadToEnd();
                        }
                    }
                }

                if (line == 2)
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    {
                        using (StreamReader rd = new StreamReader(fs, Encoding.UTF8))
                        {
                            allCurrentTableData = rd.ReadToEnd();
                            return allCurrentTableData;
                        }
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //su kien luu table image path vao "fileName"
        private void writeTableData(int line, string fileName, string browseFilePath, Rectangle rec, bool isDrag)
        {
            try
            {
                if (line == 0)
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Create))
                    {
                        using (StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8))
                        {
                            sWriter.WriteLine(browseFilePath);
                        }
                    }
                }

                if (line == 1)
                {
                    if (buttonTableCurrentNumber == 0)
                    {
                        using (FileStream fs = new FileStream(fileName, FileMode.Create))
                        {
                            using (StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8))
                            {

                            }
                        }
                    }

                    if (buttonTableCurrentNumber > 0)
                    {
                        if (!isDrag)
                        {
                            using (FileStream fs = new FileStream(fileName, FileMode.Append))
                            {
                                using (StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8))
                                {

                                }
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //browse table image
        private void btnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            string browseFileName = "";
            string browseFilePath = "";
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.DefaultExt = ".";
            browseFile.Filter = "All Image Files (*.png, *.jpg, *.jpeg)|*.png; *.jpg; *.jpeg"; // " | JPEG Files (*.jpeg)|*.jpeg | PNG Files (*.png)|*.png | JPG Files (*.jpg)|*.jpg";
            Nullable<bool> result = browseFile.ShowDialog();

            if (result == true)
            {
                browseFileName = browseFile.SafeFileName;
                browseFilePath = browseFile.FileName;

                //writeTableData(0, "tableImagePath.txt", browseFilePath, null, false);
                initBackgroundTable(browseFilePath);
            }
        }

        //method khoi tao table image
        private void initBackgroundTable(string fileName)
        {
            try
            {
                //ImageBrush backImg = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), fileName)));
                //backImg.Stretch = Stretch.Fill;
                //this.Background = backImg;
                imgTable.Source = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), fileName));
            }
            catch (Exception ex)
            {
                MessageBoxResult mess = MessageBox.Show("Table Image File: \"" + fileName + "\" is not found! Do you want to add new Table Image?", "Warning! Something went wrong!", MessageBoxButton.YesNo);
                if (mess == MessageBoxResult.Yes)
                {
                    string browseFileName = "";
                    string browseFilePath = "";
                    OpenFileDialog browseFile = new OpenFileDialog();
                    browseFile.DefaultExt = ".";
                    browseFile.Filter = "All Image Files (*.png, *.jpg, *.jpeg)|*.png; *.jpg; *.jpeg"; // " | JPEG Files (*.jpeg)|*.jpeg | PNG Files (*.png)|*.png | JPG Files (*.jpg)|*.jpg";
                    Nullable<bool> result = browseFile.ShowDialog();

                    if (result == true)
                    {
                        browseFileName = browseFile.SafeFileName;
                        browseFilePath = browseFile.FileName;

                        //writeTableData(0, "tableImagePath.txt", browseFilePath, null, false);
                        initBackgroundTable(browseFilePath);
                    }
                }
                if (mess == MessageBoxResult.No)
                {
                    //writeTableData(0, "tableImagePath.txt", @"Icon\Vector-Iluustartor.png", null, false);
                }
            }
        }

        int iii = 0;
        //them table theo vi tri
        private void btnTableButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            iii++;

            if (iii < 2)
            {
                imgTable.MouseMove += crossCursorToAdd;
                imgTable.MouseLeftButtonDown += changeToNormalCursor;
            }
        }

        int buttonTableCurrentNumber = 0;
        int maxTableCurrentNumber = 0;
        //su kien xac dinh vi tri them ban
        private void crossCursorToAdd(object sender, MouseEventArgs e)
        {
            (sender as Image).Cursor = Cursors.Cross;
        }

        //su kien chang cursor to normal
        private void changeToNormalCursor(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                maxTableCurrentNumber++;
                buttonTableCurrentNumber++;

                currentPosition = e.GetPosition(grTable);

                Rectangle rec = new Rectangle();
                if (maxTableCurrentNumber < 10)
                {
                    rec.Name = "table" + "0" + maxTableCurrentNumber.ToString();
                }
                else
                {
                    rec.Name = "table" + maxTableCurrentNumber.ToString();
                }

                rec.HorizontalAlignment = HorizontalAlignment.Left;
                rec.VerticalAlignment = VerticalAlignment.Top;
                Thickness m = rec.Margin;
                m.Left = Convert.ToInt32(currentPosition.X);
                m.Top = Convert.ToInt32(currentPosition.Y);
                rec.Margin = m;
                rec.Width = 120;
                rec.Height = 60;
                rec.Fill = Brushes.Red;
                rec.Opacity = 0.65;
                rec.ToolTip = rec.Name;

                rec.MouseLeftButtonDown += btnTableAdded_StartDrag;
                rec.MouseMove += btnTableAdded_MoveDrag;
                rec.MouseLeftButtonDown += btnTableAdded_Click;
                rec.MouseRightButtonDown += btnTableAdded_ContextMenu;

                Panel.SetZIndex(rec, 30);
                grTable.Children.Add(rec);

                (sender as Image).Cursor = Cursors.Arrow;

                imgTable.MouseMove -= crossCursorToAdd;
                imgTable.MouseLeftButtonDown -= changeToNormalCursor;
                iii = 0;

                //writeTableData(1, "tableRuntimeHistory.txt", "", rec, false);
            }
        }

        //khoi tao popup menu cua table image
        private void initcmimgTable()
        {
            ContextMenu cmRec = new ContextMenu();

            MenuItem addNewTable = new MenuItem();
            addNewTable.Name = "addNewTable";
            addNewTable.Header = "Add Table";
            addNewTable.Click += addNewTable_Click;

            cmRec.Items.Add(addNewTable);

            cmRec.PlacementTarget = imgTable;
            cmRec.IsOpen = true;
        }

        //su kien show popup menu
        private void imgTable_MouseRightContextMenu(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                currentPosition = e.GetPosition(grTable);

                initcmimgTable();
            }
        }

        //su kien khi chon add new tu popup menu
        private void addNewTable_Click(object sender, RoutedEventArgs e)
        {
            maxTableCurrentNumber++;
            buttonTableCurrentNumber++;

            Rectangle rec = new Rectangle();
            if (maxTableCurrentNumber < 10)
            {
                rec.Name = "table" + "0" + maxTableCurrentNumber.ToString();
            }
            else
            {
                rec.Name = "table" + maxTableCurrentNumber.ToString();
            }

            rec.HorizontalAlignment = HorizontalAlignment.Left;
            rec.VerticalAlignment = VerticalAlignment.Top;
            Thickness m = rec.Margin;
            m.Left = Convert.ToInt32(currentPosition.X);
            m.Top = Convert.ToInt32(currentPosition.Y);
            rec.Margin = m;
            rec.Width = 120;
            rec.Height = 60;
            rec.Fill = Brushes.Red;
            rec.Opacity = 0.65;
            rec.ToolTip = rec.Name;

            rec.MouseLeftButtonDown += btnTableAdded_StartDrag;
            rec.MouseMove += btnTableAdded_MoveDrag;
            rec.MouseLeftButtonDown += btnTableAdded_Click;
            rec.MouseRightButtonDown += btnTableAdded_ContextMenu;

            rec.Cursor = Cursors.SizeAll;

            Panel.SetZIndex(rec, 30);
            grTable.Children.Add(rec);

            imgTable.MouseMove -= crossCursorToAdd;
            imgTable.MouseLeftButtonDown -= changeToNormalCursor;
            iii = 0;

            ckeckPosition(rec, m);

            //writeTableData(1, "tableRuntimeHistory.txt", "", rec, false);
        }

        //method tao popup menu cho table
        private void initcmRec(string cmType)
        {
            ContextMenu cmRec = new ContextMenu();

            MenuItem pinTable = new MenuItem();
            pinTable.Name = "pinTable";
            pinTable.Header = "Pin Table";
            pinTable.Click += pinTable_Click;

            MenuItem moveTable = new MenuItem();
            moveTable.Name = "moveTable";
            moveTable.Header = "Move Table";
            moveTable.Click += moveTable_Click;

            MenuItem removeTable = new MenuItem();
            removeTable.Name = "removeTable";
            removeTable.Header = "Remove Table";
            removeTable.Click += removeTable_Click;

            if (cmType.Equals("pinned"))
            {
                cmRec.Items.Add(moveTable);
                cmRec.Items.Add(removeTable);
            }

            if (cmType.Equals("moved"))
            {
                cmRec.Items.Add(pinTable);
                cmRec.Items.Add(removeTable);
            }

            cmRec.PlacementTarget = currentRec;
            cmRec.IsOpen = true;
        }

        //su kien khi click table(single or double)
        private async void btnTableAdded_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                FrameworkElement ctrl = (FrameworkElement)sender;
                ClickAttach.SetClicks(ctrl, (int)e.ClickCount);
                await Task.Delay(300);
                DoClickSingleOrDouble(ctrl, sender as Rectangle);
            }
        }

        //mothod kiem tra single or double
        private void DoClickSingleOrDouble(FrameworkElement ctrl, Rectangle rec)
        {
            int clicks = ClickAttach.GetClicks(ctrl);
            ClickAttach.SetClicks(ctrl, 0);
            if (clicks > 0)
            {
                if (clicks == 1)
                {
                    //rec.MouseLeftButtonDown += btnTableAdded_StartDrag;
                    //rec.MouseMove += btnTableAdded_MoveDrag;
                }
                else if (clicks == 2)
                {
                    MessageBox.Show("Go to order with " + rec.Name);
                }
            }
        }

        Rectangle currentRec;
        //su kien khi right click table
        private void btnTableAdded_ContextMenu(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                currentRec = sender as Rectangle;

                //tao contextmenu bang xaml resources
                //ContextMenu cm = this.FindResource("cmbtntable") as ContextMenu;
                //cm.PlacementTarget = sender as Rectangle;
                //cm.IsOpen = true;

                if (currentRec.Opacity == 0.65)
                {
                    initcmRec("moved");
                }

                if (currentRec.Opacity == 1)
                {
                    initcmRec("pinned");
                }
            }
        }

        //su kien khi lua chon pin tu popup menu cua table
        private void pinTable_Click(object sender, RoutedEventArgs e)
        {
            if (currentRec.Opacity == 0.65)
            {
                currentRec.MouseLeftButtonDown -= btnTableAdded_StartDrag;
                currentRec.MouseMove -= btnTableAdded_MoveDrag;
                currentRec.Opacity = 1;

                currentRec.Cursor = Cursors.Arrow;

                string dir = "";
                string serializationFile = System.IO.Path.Combine(dir, "salesmen.bin");

                //serialize
                //using (Stream stream = File.Open(serializationFile, FileMode.Create))
                //{
                //    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //    bformatter.Serialize(stream, ItemData.ilist);
                //}

                //deserialize
                using (Stream stream = File.Open(serializationFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    List<Item> il = (List<Item>)bformatter.Deserialize(stream);

                    foreach (var i in il)
                    {
                        MessageBox.Show(i.Name + ":" + i.Price);
                    }
                }
            }
        }

        //su kien khi lua chon move tu popup menu cua table
        private void moveTable_Click(object sender, RoutedEventArgs e)
        {
            if (currentRec.Opacity == 1)
            {
                currentRec.MouseLeftButtonDown += btnTableAdded_StartDrag;
                currentRec.MouseMove += btnTableAdded_MoveDrag;
                currentRec.Opacity = 0.65;

                currentRec.Cursor = Cursors.SizeAll;
            }
        }

        //su kien khi lua chon remove tu popup menu cua table
        private void removeTable_Click(object sender, RoutedEventArgs e)
        {
            grTable.Children.Remove(currentRec);
            buttonTableCurrentNumber--;
        }

        Point startPoint;
        Point startPointInTable;
        Point currentPosition;
        //su kien bat dau drag
        private void btnTableAdded_StartDrag(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(grTable);
            startPointInTable = e.GetPosition(sender as Rectangle);
        }

        //su kien move drag
        private void btnTableAdded_MoveDrag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                (sender as Rectangle).Cursor = Cursors.SizeAll;

                currentPosition = e.GetPosition(grTable);

                if (Math.Abs(Convert.ToInt32(startPoint.X - currentPosition.X)) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(Convert.ToInt32(startPoint.Y - currentPosition.Y)) > SystemParameters.MinimumVerticalDragDistance)
                {
                    Rectangle rec = sender as Rectangle;
                    var dragData = new DataObject(typeof(Rectangle), rec);
                    DragDrop.DoDragDrop(rec, dragData, DragDropEffects.Move);
                }
            }
        }

        Point currentPointer;
        //su kien lay vi tri pointer
        private void grTable_MouseMove(object sender, MouseEventArgs e)
        {
            currentPointer = e.GetPosition(grTable);
            txbPointerPosition.Text = Convert.ToInt32(currentPointer.X) + ":" + Convert.ToInt32(currentPointer.Y);
        }

        //su kien drag di vao image
        private void imgTable_DragEnter(object sender, DragEventArgs e)
        {
            currentPosition = e.GetPosition(grTable);

            Rectangle newRec = e.Data.GetData(typeof(Rectangle)) as Rectangle;

            Thickness m = newRec.Margin;

            createNewRectangleDroped(newRec, m, currentPosition, startPointInTable);

            ckeckPosition(newRec, m);
        }

        //su kien drag ra khoi vung quy dinh
        private void imgTable_DragLeave(object sender, DragEventArgs e)
        {

        }

        //su kien drop ractangle vao table image
        private void imgTable_Drop(object sender, DragEventArgs e)
        {
            currentPosition = e.GetPosition(grTable);

            Rectangle newRec = e.Data.GetData(typeof(Rectangle)) as Rectangle;

            Thickness m = newRec.Margin;

            createNewRectangleDroped(newRec, m, currentPosition, startPointInTable);

            ckeckPosition(newRec, m);
        }

        //method tao recrangle moi
        private void createNewRectangleDroped(Rectangle newRec, Thickness m, Point currentPosition, Point startPointInTable)
        {
            newRec.HorizontalAlignment = HorizontalAlignment.Left;
            newRec.VerticalAlignment = VerticalAlignment.Top;

            m.Left = Convert.ToInt32(currentPosition.X) - Convert.ToInt32(startPointInTable.X);
            m.Top = Convert.ToInt32(currentPosition.Y) - Convert.ToInt32(startPointInTable.Y);
            newRec.Margin = m;
        }

        //method kiem tra vi tri table
        private void ckeckPosition(Rectangle newRec, Thickness m)
        {
            if (currentPosition.X < 0)
            {
                if (currentPosition.Y < 0)
                {
                    m.Left = 0;
                    m.Top = 0;
                    newRec.Margin = m;
                }

                if (currentPosition.Y > (660 - newRec.Height))
                {
                    m.Left = 0;
                    m.Top = 660 - newRec.Height;
                    newRec.Margin = m;
                }

                if ((currentPosition.Y > 0 && currentPosition.Y < (660 - newRec.Height)))
                {
                    m.Left = 0;
                    m.Top = Convert.ToInt32(currentPosition.Y);
                    newRec.Margin = m;
                }
            }

            if (currentPosition.X > (1366 - newRec.Width))
            {
                if (currentPosition.Y < 0)
                {
                    m.Left = 1366 - newRec.Width;
                    m.Top = 0;
                    newRec.Margin = m;
                }

                if (currentPosition.Y > (660 - newRec.Height))
                {
                    m.Left = 1366 - newRec.Width;
                    m.Top = 660 - newRec.Height;
                    newRec.Margin = m;
                }

                if ((currentPosition.Y > 0 && currentPosition.Y < (660 - newRec.Height)))
                {
                    m.Left = 1366 - newRec.Width;
                    m.Top = Convert.ToInt32(currentPosition.Y);
                    newRec.Margin = m;
                }
            }

            if (currentPosition.Y < 0 && (currentPosition.X > 0 && currentPosition.X < (1366 - newRec.Width)))
            {
                m.Left = Convert.ToInt32(currentPosition.X);
                m.Top = 0;
                newRec.Margin = m;
            }

            if (currentPosition.Y > (660 - newRec.Height) && (currentPosition.X > 0 && currentPosition.X < (1366 - newRec.Width)))
            {
                m.Left = Convert.ToInt32(currentPosition.X);
                m.Top = 660 - newRec.Height;
                newRec.Margin = m;
            }
        }

    }

    //class de ghi nhan so lan click
    public class ClickAttach : FrameworkElement
    {
        public static int GetClicks(FrameworkElement ctrl)
        {
            return (int)ctrl.GetValue(ClicksProperty);
        }
        public static void SetClicks(FrameworkElement ctrl, int value)
        {
            ctrl.SetValue(ClicksProperty, value);
        }

        public static readonly DependencyProperty ClicksProperty = DependencyProperty.RegisterAttached("Clicks", typeof(int), typeof(FrameworkElement));
    }

}
