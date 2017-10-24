using Microsoft.Win32;
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
            initBackgroundTable(currentTableImage(0));
        }

        //lay thong tin table image, tat ca table hien co
        private string currentTableImage(int line) //0: tableImagePath; 1: tableRuntimeHistory
        {
            try
            {
                if (line == 0)
                {
                    using (FileStream fs = new FileStream("tableImagePath.txt", FileMode.Open))
                    using (StreamReader rd = new StreamReader(fs, Encoding.UTF8))
                    {
                        string tableImagePath = rd.ReadLine();
                        return tableImagePath;
                    }
                }

                if (line == 1)
                {
                    using (FileStream fs = new FileStream("tableRuntimeHistory.txt", FileMode.Open))
                    using (StreamReader rd = new StreamReader(fs, Encoding.UTF8))
                    {
                        string tableRuntime = rd.ReadToEnd();
                        return tableRuntime;
                    }
                }

                return "";
            }
            catch (FileNotFoundException)
            {
                return "";
            }
        }

        //su kien luu table image path vao "fileName"
        private void writeTableImage(string fileName, string browseFilePath)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            using (StreamWriter sWriter = new StreamWriter(fs, Encoding.UTF8))
            {
                sWriter.WriteLine(browseFilePath);
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

                writeTableImage("tableImagePath.txt", browseFilePath);
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

                        writeTableImage("tableImagePath.txt", browseFilePath);
                        initBackgroundTable(browseFilePath);
                    }
                }
                if (mess == MessageBoxResult.No)
                {
                    writeTableImage("tableImagePath.txt", @"Icon\Vector-Iluustartor.png");
                }
            }
        }

        //them table theo vi tri
        private void btnTableButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            imgTable.MouseMove += crossCursorToAdd;
            imgTable.MouseLeftButtonDown += changeToNormalCursor;
        }

        int buttonTableNumber = 0;
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
                buttonTableNumber++;

                currentPosition = e.GetPosition(grTable);
                Rectangle rec = new Rectangle();
                if (buttonTableNumber < 10)
                {
                    rec.Name = "table" + "0" + buttonTableNumber.ToString();
                }
                else
                {
                    rec.Name = "table" + buttonTableNumber.ToString();
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
            }
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
                    rec.MouseLeftButtonDown += btnTableAdded_StartDrag;
                    rec.MouseMove += btnTableAdded_MoveDrag;
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

                ContextMenu cm = this.FindResource("cmbtntable") as ContextMenu;
                cm.PlacementTarget = sender as Rectangle;
                cm.IsOpen = true;
            }
        }

        //su kien khi lua chon move tu popup menu cua table
        private void moveTable_Click(object sender, RoutedEventArgs e)
        {

        }

        //su kien khi lua chon remove tu popup menu cua table
        private void removeTable_Click(object sender, RoutedEventArgs e)
        {
            grTable.Children.Remove(currentRec);
        }

        Point startPoint;
        Point currentPosition;
        //su kien bat dau drag
        private void btnTableAdded_StartDrag(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        //su kien move drag
        private void btnTableAdded_MoveDrag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                currentPosition = e.GetPosition(grTable);
                if (Math.Abs(Convert.ToInt32(startPoint.X - currentPosition.X)) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(Convert.ToInt32(startPoint.Y - currentPosition.Y)) > SystemParameters.MinimumVerticalDragDistance)
                {
                    Rectangle rec = e.Source as Rectangle;
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
            currentPosition = e.GetPosition(imgTable);
            txbPointerPosition.Text = Convert.ToInt32(currentPosition.X) + ":" + Convert.ToInt32(currentPosition.Y);
        }

        //su kien drag ra khoi vung quy dinh
        private void imgTable_DragLeave(object sender, DragEventArgs e)
        {

        }

        //su kien drop ractangle vao table image
        private void imgTable_Drop(object sender, DragEventArgs e)
        {
            Rectangle newRec = e.Data.GetData(typeof(Rectangle)) as Rectangle;

            newRec.HorizontalAlignment = HorizontalAlignment.Left;
            newRec.VerticalAlignment = VerticalAlignment.Top;
            Thickness m = newRec.Margin;
            m.Left = Convert.ToInt32(currentPosition.X);
            m.Top = Convert.ToInt32(currentPosition.Y);
            newRec.Margin = m;

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

            if (currentPosition.Y > 660 && (currentPosition.X > 0 && currentPosition.X < (1366 - newRec.Width)))
            {
                m.Left = Convert.ToInt32(currentPosition.X);
                m.Top = 660 - newRec.Height;
                newRec.Margin = m;
            }

            //newRec.Width = 120;
            //newRec.Height = 60;
            //newRec.Fill = Brushes.Red;
            //newRec.MouseLeftButtonDown += btnAddTable_StartDrag;
            //newRec.MouseMove += btnAddTable_MoveDrag;

            //Panel.SetZIndex(newRec, 30);
            //grTable.Children.Add(newRec);
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
