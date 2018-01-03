using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using POS.BusinessModel;
using POS.Repository.DAL;
using System.Linq;
using System.Windows.Media.Effects;
using POS.Entities;

namespace POS.EmployeeWorkSpace
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : Page
    {
        string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private EmployeewsOfLocalPOS _unitofwork;
        private EmployeewsOfCloudPOS _cloudPosUnitofwork;
        public List<Entities.Table> currentTableList;
        private DropShadowBitmapEffect recShadow, recShadowOrdered;

        public Table(EmployeewsOfLocalPOS unitofwork, EmployeewsOfCloudPOS cloudPosUnitofwork)
        {
            _unitofwork = unitofwork;
            _cloudPosUnitofwork = cloudPosUnitofwork;
            InitializeComponent();

            recShadow = new DropShadowBitmapEffect
            {
                Color = new Color { A = 1, R = 255, G = 0, B = 0 },
                ShadowDepth = 5,
                Direction = 270,
                Softness = 0.60,
                Opacity = 0.75
            };

            recShadowOrdered = new DropShadowBitmapEffect
            {
                Color = new Color { A = 1, R = 0, G = 55, B = 55 },
                ShadowDepth = 5,
                Direction = 270,
                Softness = 0.60,
                Opacity = 0.75
            };

            initTableData();

            Loaded += TablePage_loaded;
        }


        internal bool isTablesDataChange = true;
        public void TablePage_loaded(Object sender, EventArgs args)
        {
            if (isTablesDataChange)
            {
                //currentTableList = _unitofwork.TableRepository.Get().ToList();
                ((MainWindow)Window.GetWindow(this)).initProgressTableChair();
            }

            foreach (Entities.Table t in currentTableList)
            {
                Rectangle rec = t.TableRec;

                //if (t.IsPinned == 1)
                //{
                //    rec.MouseLeftButtonDown -= btnTableAdded_StartDrag;
                //    rec.MouseMove -= btnTableAdded_MoveDrag;
                //    rec.Opacity = 1;
                //    rec.Cursor = Cursors.Arrow;
                //    rec.SetValue(BitmapEffectProperty, recShadow);
                //}
                //else
                //{
                //    rec.Cursor = Cursors.SizeAll;
                //    rec.Fill = Brushes.Red;
                //    rec.Opacity = 0.65;
                //}

                if (t.IsOrdered == 1)
                {
                    rec.Fill = Brushes.DarkCyan;
                    rec.SetValue(BitmapEffectProperty, recShadowOrdered);
                }
                else
                {
                    if (t.IsPinned != 0)
                    {
                        rec.SetValue(BitmapEffectProperty, recShadow);
                    }

                    rec.Fill = Brushes.Red;
                }

                if (isTablesDataChange)
                {
                    //Image img = new Image { Source = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "..\\Icon\\icons8_Meal_32px.png")) };
                    //img.Width = 32;
                    //img.Height = 32;
                    //Thickness m;
                    //m = img.Margin;
                    //m.Left = rec.Margin.Left;
                    //m.Top = rec.Margin.Top;
                    //img.Margin = m;
                    //grTable.Children.Add(img);

                    //rec.Fill = new ImageBrush
                    //{
                    //    ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "..\\Icon\\icons8_Meal_32px.png"))
                    //};

                    rec.ToolTip = SetTooltip(rec);
                }
            }

            isTablesDataChange = false;
        }

        //load table data
        private void initTableData()
        {
            if (!ReadWriteData.checkTableSizeFileExist())
            {
                ReadWriteData.writeTableSize("30-30");
            }

            if (!ReadWriteData.checkTableImagePathFileExist())
            {
                ReadWriteData.writeTableImagePath(startupProjectPath + "\\Images\\3dmap.png");
            }

            initBackgroundTable(ReadWriteData.readTableImagePath());

            currentTableList = _unitofwork.TableRepository.Get().ToList();

            readTableData();
        }

        //lay thong tin table image, tat ca table hien co
        private void readTableData() //0: tableImagePath; 1: tableRuntimeHistory
        {
            Rectangle rec;
            Thickness m;

            foreach (Entities.Table t in currentTableList)
            {
                if (maxTableCurrentNumber < t.TableNumber)
                {
                    maxTableCurrentNumber = t.TableNumber;
                }
            }

            foreach (Entities.Table t in currentTableList)
            {
                buttonTableCurrentNumber++;

                rec = new Rectangle();
                if (t.TableNumber < 10)
                {
                    rec.Name = "table" + "0" + t.TableNumber;
                }
                else
                {
                    rec.Name = "table" + t.TableNumber;
                }

                rec.HorizontalAlignment = HorizontalAlignment.Left;
                rec.VerticalAlignment = VerticalAlignment.Top;
                m = rec.Margin;
                m.Left = Convert.ToInt32(t.PosX);
                m.Top = Convert.ToInt32(t.PosY);
                rec.Margin = m;
                rec.Width = int.Parse(ReadWriteData.readTableSize()[0]);
                rec.Height = int.Parse(ReadWriteData.readTableSize()[1]);
                rec.Fill = Brushes.Red;
                rec.Opacity = 0.65;

                //ImageBrush backImg = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "D:\\icons8_Pin_32px_9.png")));
                //ImageBrush backImg = new ImageBrush(new BitmapImage(new Uri(@"/Icon/icons8_Pin_32px_9.png", UriKind.RelativeOrAbsolute)));
                //backImg.Stretch = Stretch.Fill;
                //rec.Fill = backImg;

                rec.MouseLeftButtonDown += btnTableAdded_StartDrag;
                rec.MouseMove += btnTableAdded_MoveDrag;
                rec.MouseLeftButtonDown += btnTableAdded_Click;

                rec.Cursor = Cursors.SizeAll;

                imgTable.MouseMove -= crossCursorToAdd;
                imgTable.MouseLeftButtonDown -= changeToNormalCursor;
                iii = 0;

                if (t.IsPinned == 1)
                {
                    rec.MouseLeftButtonDown -= btnTableAdded_StartDrag;
                    rec.MouseMove -= btnTableAdded_MoveDrag;
                    rec.Opacity = 1;
                    rec.Cursor = Cursors.Arrow;
                    rec.SetValue(BitmapEffectProperty, recShadow);
                }
                else
                {
                    rec.Cursor = Cursors.SizeAll;
                    rec.Fill = Brushes.Red;
                    rec.Opacity = 0.65;
                }

                if (t.IsOrdered == 1)
                {
                    rec.Fill = Brushes.DarkCyan;
                    rec.SetValue(BitmapEffectProperty, recShadowOrdered);
                }
                else
                {
                    rec.Fill = Brushes.Red;
                }

                rec.MouseMove += btnTableAdded_MouseMove;
                rec.MouseRightButtonDown += btnTableAdded_ContextMenu;
                rec.MouseLeave += btnTableAdded_MouseLeave;

                Panel.SetZIndex(rec, 100);
                grTable.Children.Add(rec);

                rec.ToolTip = SetTooltip(rec);

                t.TableRec = rec;
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

                ReadWriteData.writeTableImagePath(browseFilePath);
                initBackgroundTable(ReadWriteData.readTableImagePath());
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

                        ReadWriteData.writeTableImagePath(browseFilePath);
                        initBackgroundTable(ReadWriteData.readTableImagePath());
                    }
                }
                if (mess == MessageBoxResult.No)
                {
                    ReadWriteData.writeTableImagePath("C:\\Program Files\\ITComma\\Asowel POS\\documents\\3dmap.jpg");
                    initBackgroundTable(ReadWriteData.readTableImagePath());
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
                rec.Width = int.Parse(ReadWriteData.readTableSize()[0]);
                rec.Height = int.Parse(ReadWriteData.readTableSize()[1]);
                rec.Fill = Brushes.Red;
                rec.Opacity = 0.65;

                rec.MouseLeftButtonDown += btnTableAdded_StartDrag;
                rec.MouseMove += btnTableAdded_MoveDrag;
                rec.MouseLeftButtonDown += btnTableAdded_Click;
                rec.MouseRightButtonDown += btnTableAdded_ContextMenu;

                rec.MouseMove += btnTableAdded_MouseMove;
                rec.MouseLeave += btnTableAdded_MouseLeave;

                Panel.SetZIndex(rec, 30);
                grTable.Children.Add(rec);

                (sender as Image).Cursor = Cursors.Arrow;

                imgTable.MouseMove -= crossCursorToAdd;
                imgTable.MouseLeftButtonDown -= changeToNormalCursor;
                iii = 0;

                ((MainWindow)Window.GetWindow(this)).proTable.Maximum += 1;

                currentTableList.Add(ReadWriteData.writeOnAddNew(_unitofwork, rec, App.Current.Properties["EmpLogin"] as Entities.Employee));

                ((MainWindow)Window.GetWindow(this)).initProgressTableChair();

                rec.ToolTip = SetTooltip(rec);
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
            rec.Width = int.Parse(ReadWriteData.readTableSize()[0]);
            rec.Height = int.Parse(ReadWriteData.readTableSize()[1]);
            rec.Fill = Brushes.Red;
            rec.Opacity = 0.65;

            rec.MouseLeftButtonDown += btnTableAdded_StartDrag;
            rec.MouseMove += btnTableAdded_MoveDrag;
            rec.MouseLeftButtonDown += btnTableAdded_Click;
            rec.MouseRightButtonDown += btnTableAdded_ContextMenu;

            rec.MouseMove += btnTableAdded_MouseMove;
            rec.MouseLeave += btnTableAdded_MouseLeave;

            rec.Cursor = Cursors.SizeAll;

            Panel.SetZIndex(rec, 30);
            grTable.Children.Add(rec);

            imgTable.MouseMove -= crossCursorToAdd;
            imgTable.MouseLeftButtonDown -= changeToNormalCursor;
            iii = 0;

            ckeckPosition(rec, m);

            ((MainWindow)Window.GetWindow(this)).proTable.Maximum += 1;

            currentTableList.Add(ReadWriteData.writeOnAddNew(_unitofwork, rec, App.Current.Properties["EmpLogin"] as Entities.Employee));

            rec.ToolTip = SetTooltip(rec);
        }

        //method tao popup menu cho table
        private void initcmRec(string cmType, Entities.Table t)
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

            MenuItem changeChairTable = new MenuItem();
            changeChairTable.Name = "changeChairTable";
            changeChairTable.Header = "Change Table Chair Amount";
            changeChairTable.Click += changeChairTable_Click;

            MenuItem removeTable = new MenuItem();
            removeTable.Name = "removeTable";
            removeTable.Header = "Remove Table";
            removeTable.Click += removeTable_Click;

            //demo payed
            //MenuItem payedTable = new MenuItem();
            //payedTable.Name = "payedTable";
            //payedTable.Header = "Payed Table";
            //payedTable.Click += payedTable_Click;

            if (cmType.Equals("pinned"))
            {
                if (t.IsOrdered == 1)
                {
                    cmRec.Items.Add(changeChairTable);
                    cmRec.Items.Add(removeTable);
                    //cmRec.Items.Add(payedTable);
                }
                else if (t.IsPinned == 1)
                {
                    cmRec.Items.Add(moveTable);
                    cmRec.Items.Add(changeChairTable);
                    cmRec.Items.Add(removeTable);
                }
            }

            if (cmType.Equals("moved"))
            {
                cmRec.Items.Add(pinTable);
                cmRec.Items.Add(changeChairTable);
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
            if (clicks == 2)
            {
                if (App.Current.Properties["AdLogin"] != null)
                {
                    return;
                }

                EmpLoginList currentEmp = App.Current.Properties["CurrentEmpWorking"] as EmpLoginList;

                AllEmployeeLogin ael;
                Entities.Table founded = currentTableList.Where(x => x.TableNumber.Equals(int.Parse(rec.Name.Substring(5)))).First();
                if (founded == null)
                {
                    return;
                }

                var ordertempcurrenttable = _unitofwork.OrderTempRepository.Get(x => x.TableOwned.Equals(founded.TableId)).First();

                if (founded.IsPinned == 0)
                {
                    MessageBoxResult mess = MessageBox.Show("You must be pin this table before you want to create new order. Do you want to pin now?", "Warning!", MessageBoxButton.YesNo);
                    if (mess == MessageBoxResult.Yes)
                    {
                        if (founded.ChairAmount == 0)
                        {
                            MessageBox.Show("You must be set Chair Amount greater than 0!");
                            return;
                        }

                        if (currentEmp != null)
                        {
                            if (ordertempcurrenttable != null)
                            {
                                ordertempcurrenttable.EmpId = currentEmp.Emp.EmpId;
                                _unitofwork.OrderTempRepository.Update(ordertempcurrenttable);
                                _unitofwork.Save();
                            }

                            navigateToOrder(currentEmp, founded.TableRec, founded);
                            return;
                        }

                        ael = new AllEmployeeLogin((MainWindow)Window.GetWindow(this), _unitofwork, _cloudPosUnitofwork, ((MainWindow)Window.GetWindow(this)).cUser, 4);
                        ael.ShowDialog();

                        checkCurrentEmp(currentEmp, founded.TableRec, founded, ordertempcurrenttable);
                    }
                }
                else
                {
                    if (founded.ChairAmount == 0)
                    {
                        MessageBox.Show("You must be set Chair Amount greater than 0!");
                        return;
                    }

                    if (founded.IsOrdered == 1)
                    {
                        currentEmp = App.Current.Properties["CurrentEmpWorking"] as EmpLoginList;

                        if (currentEmp != null)
                        {
                            if (currentEmp.Emp.EmpId.Equals(ordertempcurrenttable.EmpId))
                            {
                                navigateToOrder(currentEmp, founded.TableRec, founded);
                                return;
                            }
                            else
                            {
                                navigateToOrder(currentEmp, founded.TableRec, founded);
                                return;
                            }
                        }

                        ael = new AllEmployeeLogin((MainWindow)Window.GetWindow(this), _unitofwork, _cloudPosUnitofwork, ((MainWindow)Window.GetWindow(this)).cUser, 4);
                        ael.ShowDialog();

                        checkCurrentEmp(currentEmp, founded.TableRec, founded, ordertempcurrenttable);
                    }
                    else
                    {
                        currentEmp = App.Current.Properties["CurrentEmpWorking"] as EmpLoginList;

                        if (currentEmp != null)
                        {
                            if (ordertempcurrenttable != null)
                            {
                                ordertempcurrenttable.EmpId = currentEmp.Emp.EmpId;
                                _unitofwork.OrderTempRepository.Update(ordertempcurrenttable);
                                _unitofwork.Save();
                            }

                            checkCurrentEmp(currentEmp, founded.TableRec, founded, ordertempcurrenttable);
                            return;
                        }

                        ael = new AllEmployeeLogin((MainWindow)Window.GetWindow(this), _unitofwork, _cloudPosUnitofwork, ((MainWindow)Window.GetWindow(this)).cUser, 4);
                        ael.ShowDialog();

                        checkCurrentEmp(currentEmp, founded.TableRec, founded, ordertempcurrenttable);
                    }
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

                foreach (Entities.Table t in currentTableList)
                {
                    if (t.TableNumber == int.Parse(currentRec.Name.Substring(5)))
                    {
                        if (t.IsPinned == 1)
                        {
                            initcmRec("pinned", t);
                        }
                        else
                        {
                            initcmRec("moved", t);
                        }
                    }
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

                Entities.Table t = currentTableList.Where(x => x.TableNumber.Equals(int.Parse(currentRec.Name.Substring(5)))).First();
                t.IsPinned = 1;
                currentRec.SetValue(BitmapEffectProperty, recShadow);

                _unitofwork.TableRepository.Update(t);
                _unitofwork.Save();

                currentRec.ToolTip = SetTooltip(currentRec);
            }
        }

        //su kien khi lua chon move tu popup menu cua table
        private void moveTable_Click(object sender, RoutedEventArgs e)
        {
            bool pass = false;

            if (currentRec.Opacity == 1)
            {
                if (App.Current.Properties["AdLogin"] == null)
                {
                    MessageBoxResult mess = MessageBox.Show("You must have higher permission for this action? Do you want to continue?", "Warning!", MessageBoxButton.YesNo);
                    if (mess == MessageBoxResult.Yes)
                    {
                        PermissionRequired pr = new PermissionRequired(_cloudPosUnitofwork, ((MainWindow)Window.GetWindow(this)).cUser);
                        pr.ShowDialog();

                        if (App.Current.Properties["AdLogin"] != null)
                        {
                            pass = true;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    pass = true;
                }

                if (pass)
                {
                    currentRec.MouseLeftButtonDown += btnTableAdded_StartDrag;
                    currentRec.MouseMove += btnTableAdded_MoveDrag;
                    currentRec.Opacity = 0.65;

                    currentRec.Cursor = Cursors.SizeAll;

                    Entities.Table t = currentTableList.Where(x => x.TableNumber.Equals(int.Parse(currentRec.Name.Substring(5)))).First();
                    t.IsPinned = 0;

                    currentRec.ClearValue(BitmapEffectProperty);

                    _unitofwork.TableRepository.Update(t);
                    _unitofwork.Save();

                    currentRec.ToolTip = SetTooltip(currentRec);
                }
            }
        }

        //su kien khi chon change chair tu table
        private void changeChairTable_Click(object sender, RoutedEventArgs e)
        {
            var t = currentTableList.Where(x => x.TableNumber.Equals(int.Parse(currentRec.Name.Substring(5)))).First();

            TableSettingDialog tableSetting = new TableSettingDialog(_unitofwork, t);
            tableSetting.ShowDialog();

            _unitofwork.TableRepository.Update(t);
            _unitofwork.Save();

            currentRec.ToolTip = SetTooltip(currentRec);
            ((MainWindow)Window.GetWindow(this)).initProgressTableChair();
        }

        //su kien khi lua chon remove tu popup menu cua table
        private void removeTable_Click(object sender, RoutedEventArgs e)
        {
            bool pass = false;
            if (App.Current.Properties["AdLogin"] == null)
            {
                MessageBoxResult mess = MessageBox.Show("You must have higher permission for this action? Do you want to continue?", "Warning!", MessageBoxButton.YesNo);
                if (mess == MessageBoxResult.Yes)
                {
                    PermissionRequired pr = new PermissionRequired(_cloudPosUnitofwork, ((MainWindow)Window.GetWindow(this)).cUser);
                    pr.ShowDialog();

                    if (App.Current.Properties["AdLogin"] != null)
                    {
                        pass = true;
                    }
                }
                else
                {
                    pass = false;
                }
            }
            else
            {
                pass = true;
            }

            if (pass)
            {
                var t = currentTableList.Where(x => x.TableNumber.Equals(int.Parse(currentRec.Name.Substring(5)))).First();

                if (t.TableNumber == int.Parse(currentRec.Name.Substring(5)) && t.IsOrdered == 1)
                {
                    MessageBox.Show("This table is ordering! You can not remove this table");
                    return;
                }

                if (t.TableNumber == int.Parse(currentRec.Name.Substring(5)) && t.IsOrdered == 0)
                {
                    var chairlist = _unitofwork.ChairRepository.Get(x => x.TableOwned.Equals(t.TableId)).ToList();
                    var ordertemptable = _unitofwork.OrderTempRepository.Get(x => x.TableOwned.Equals(t.TableId)).First();
                    foreach (var ch in chairlist)
                    {
                        _unitofwork.ChairRepository.Delete(ch);
                    }
                    _unitofwork.OrderTempRepository.Delete(ordertemptable);
                    _unitofwork.TableRepository.Delete(t);
                    _unitofwork.Save();
                    grTable.Children.Remove(currentRec);
                    buttonTableCurrentNumber--;
                    return;
                }
                ((MainWindow)Window.GetWindow(this)).proTable.Maximum -= 1;
            }
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

                return;
            }
        }

        Brush ori;
        bool checkMove = true;
        //su kien khi move qua Table
        private void btnTableAdded_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkMove)
            {
                ori = (sender as Rectangle).Fill;

                currentRec = new Rectangle();
                currentRec = sender as Rectangle;

                //(sender as Rectangle).ToolTip = SetTooltip(sender as Rectangle);
                if (currentRec.Fill == Brushes.Red)
                {
                    //currentRec.Fill = (Brush)(new BrushConverter()).ConvertFromString("#CC000000");
                    currentRec.Fill = new SolidColorBrush(Color.FromRgb(220, 0, 0));
                }
                else if (currentRec.Fill == Brushes.DarkCyan)
                {
                    //currentRec.Fill = (Brush)(new BrushConverter()).ConvertFromString("#00CCFF00");
                    currentRec.Fill = new SolidColorBrush(Color.FromRgb(0, 110, 110));
                }

                checkMove = false;
            }
        }

        //su kien khi move out Table
        private void btnTableAdded_MouseLeave(object sender, MouseEventArgs e)
        {
            currentRec.Fill = ori;
            checkMove = true;
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

            int tabNum = int.Parse(newRec.Name.Substring(5));

            var curTable = _unitofwork.TableRepository.Get(x => x.TableNumber.Equals(tabNum)).First();
            if (curTable != null)
            {
                curTable.PosX = Convert.ToInt32(newRec.Margin.Left);
                curTable.PosY = Convert.ToInt32(newRec.Margin.Top);
            }

            _unitofwork.TableRepository.Update(curTable);
            _unitofwork.Save();
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

        //method set tooltip cho table
        private string SetTooltip(Rectangle rec)
        {
            foreach (Entities.Table table in currentTableList)
            {
                if (table.TableNumber == int.Parse(rec.Name.Substring(5)))
                {
                    var ordertemptable = _unitofwork.OrderTempRepository.Get(x => x.TableOwned.Equals(table.TableId)).First();
                    var orderdetailstemptable = _unitofwork.OrderDetailsTempRepository.Get(x => x.OrdertempId.Equals(ordertemptable.OrdertempId)).ToList();

                    string tt = "Table Number: " + table.TableNumber;
                    tt += "\nChair Amount: " + table.ChairAmount;
                    //tt += "\nPosition(W:H): " + Convert.ToInt32(table.Position.X) + ":" + Convert.ToInt32(table.Position.Y);

                    if (table.IsOrdered == 1)
                    {
                        Customer cus = null;
                        if ((cus = _cloudPosUnitofwork.CustomerRepository.Get(x => x.CusId.Equals(ordertemptable.CusId)).FirstOrDefault()) != null)
                        {
                            tt += "\nOrder Customer: " + cus.Name;
                        }

                        foreach (var tableOD in orderdetailstemptable)
                        {
                            Product prod = null;
                            if ((prod = _cloudPosUnitofwork.ProductRepository.Get(x => x.ProductId.Equals(tableOD.ProductId)).FirstOrDefault()) != null)
                            {
                                tt += "\nProduct Name: " + prod.Name;
                            }

                            tt += ", Quantity: " + tableOD.Quan;
                        }
                    }

                    if (table.IsPinned == 1)
                    {
                        tt += "\nPinned";
                    }
                    else
                    {
                        tt += "\nMoved";
                    }

                    return tt;
                }
            }

            return "";
        }

        //method kiem tra sau khi start employee -> order
        private void checkCurrentEmp(EmpLoginList currentEmp, Rectangle rec, Entities.Table founded, OrderTemp ordertempcurrenttable)
        {
            if (App.Current.Properties["CurrentEmpWorking"] == null)
            {
                return;
            }

            currentEmp = App.Current.Properties["CurrentEmpWorking"] as EmpLoginList;

            if (currentEmp != null)
            {
                if (ordertempcurrenttable != null)
                {
                    ordertempcurrenttable.EmpId = currentEmp.Emp.EmpId;
                    _unitofwork.OrderTempRepository.Update(ordertempcurrenttable);
                    _unitofwork.Save();
                }

                navigateToOrder(currentEmp, rec, founded);
                return;
            }
        }

        private void btnTableButtonSwap_Click(object sender, RoutedEventArgs e)
        {
            //if(App.Current.Properties["CurrentEmpWorking"] == null)
            //{
            //    MessageBox.Show("You must be start working for this action!");
            //    return;
            //}

            SwapOrMergeTable smt = new SwapOrMergeTable(_unitofwork, currentTableList, 1);
            smt.ShowDialog();

            //foreach(var rec in currentTableList)
            //{
            //    Image img = new Image { Source = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "..\\Icon\\icons8_Meal_32px.png")) };
            //    img.Width = 32;
            //    img.Height = 32;
            //    Thickness m;
            //    m = img.Margin;
            //    m.Left = rec.TableRec.Margin.Left;
            //    m.Top = rec.TableRec.Margin.Top;
            //    img.Margin = m;
            //    grTable.Children.Add(img);
            //    MessageBox.Show(rec.PosX + ":" + rec.PosY);
            //    MessageBox.Show(img.Margin.Left + ":" + img.Margin.Top);
            //}

            //currentRec.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "..\\Icon\\icons8_Meal_32px.png")) };
        }

        private void btnTableButtonMerge_Click(object sender, RoutedEventArgs e)
        {
            SwapOrMergeTable smt = new SwapOrMergeTable(_unitofwork, currentTableList, 2);
            smt.ShowDialog();

            isTablesDataChange = true;
            var en = (Entry)((MainWindow)Window.GetWindow(this)).en;
            var tab = (Table)((MainWindow)Window.GetWindow(this)).b;
            ((MainWindow)Window.GetWindow(this)).myFrame.Navigate(en);
            ((MainWindow)Window.GetWindow(this)).myFrame.Navigate(tab);
        }

        //method navigate to entry page
        private void navigateToOrder(EmpLoginList currentEmp, Rectangle rec, Entities.Table founded)
        {
            rec.MouseLeftButtonDown -= btnTableAdded_StartDrag;
            rec.MouseMove -= btnTableAdded_MoveDrag;
            rec.Opacity = 1;
            rec.Cursor = Cursors.Arrow;
            rec.Fill = Brushes.DarkCyan;
            rec.SetValue(BitmapEffectProperty, recShadowOrdered);

            founded.IsPinned = 1;

            //pass
            ((MainWindow)Window.GetWindow(this)).currentTable = founded;
            var orderControl = (Entry)((MainWindow)Window.GetWindow(this)).en;
            ((MainWindow)Window.GetWindow(this)).myFrame.Navigate(orderControl);
            orderControl.ucOrder.RefreshControl(_unitofwork, founded);
            ((MainWindow)Window.GetWindow(this)).bntTable.IsEnabled = true;
            ((MainWindow)Window.GetWindow(this)).bntDash.IsEnabled = true;
            ((MainWindow)Window.GetWindow(this)).bntEntry.IsEnabled = false;

            _unitofwork.TableRepository.Update(founded);
            _unitofwork.Save();
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
