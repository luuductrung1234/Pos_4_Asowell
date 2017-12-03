using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using POS.Entities.CustomEntities;
using POS.Repository.DAL;

namespace POS.Helper.PrintHelper
{
    public class EndOfDayPrintHelper : IPrintHelper
    {
        private EmployeewsOfCloudPOS _cloudPosUnitofwork;
        private static string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public EndOfDayPrintHelper(EmployeewsOfCloudPOS cloudPosUnitofwork)
        {
            _cloudPosUnitofwork = cloudPosUnitofwork;
            From = DateTime.Now.Date;
            To = DateTime.Now.Date;
            To = To.AddDays(1);
        }

        public FlowDocument CreateDocument()
        {
            return CreateEndOfDayDocument();
        }

        public FlowDocument CreateEndOfDayDocument()
        {
            // Create a FlowDocument
            FlowDocument doc = new FlowDocument();

            // Set Margin
            doc.PagePadding = new Thickness(0);


            // Set PageHeight and PageWidth to "Auto".
            doc.PageHeight = Double.NaN;
            doc.PageWidth = 290;

            // Create a Section
            Section sec = new Section();


            // Head Text
            BlockUIContainer blkHeadText = new BlockUIContainer();
            Generate_HeadText(blkHeadText);


            // Table Total Sales Text
            BlockUIContainer blkTableTotalSalesText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_TableTotalSalesText(blkTableTotalSalesText);


            // Table Payment And Refund Text
            BlockUIContainer blkTablePayAndRefundText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_TablePayAndRefundText(blkTablePayAndRefundText);


            // Table Receipt Total Text
            BlockUIContainer blkTableReceiptText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_TableReceiptText(blkTableReceiptText);


            //// Add Paragraph to Section
            //sec.Blocks.Add(p1);
            sec.Blocks.Add(blkHeadText);
            sec.Blocks.Add(blkTableTotalSalesText);
            sec.Blocks.Add(blkTablePayAndRefundText);
            sec.Blocks.Add(blkTableReceiptText);


            // Add Section to FlowDocument
            doc.Blocks.Add(sec);


            return doc;
        }


        private void Generate_HeadText(BlockUIContainer blkHeadText)
        {
            // Main stackPanel of Head Text
            StackPanel stpHeadText = new StackPanel()
            {
                Orientation = Orientation.Vertical
            };

            StackPanel stpLogo = new StackPanel();
            Image imgOwner = new Image();
            BitmapImage bimg = new BitmapImage();
            bimg.BeginInit();
            bimg.UriSource = new Uri(startupProjectPath + "\\Images\\logo.png", UriKind.Absolute);
            bimg.EndInit();
            imgOwner.Source = bimg;
            imgOwner.HorizontalAlignment = HorizontalAlignment.Left;
            imgOwner.Margin = new Thickness(85, 0, 0, 0);
            stpLogo.Children.Add(imgOwner);

            StackPanel stpPageName = new StackPanel();
            TextBlock txtPageName = new TextBlock()
            {
                Text = "END OF DAY REPORT",
                FontSize = 13,
                FontFamily = new FontFamily("Century Gothic"),
                FontWeight = FontWeights.UltraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            stpPageName.Children.Add(txtPageName);

            TextBlock txtFrom = new TextBlock()
            {
                Text = "From: " + From.ToShortDateString(),
                FontSize = 11,
                FontFamily = new FontFamily("Century Gothic"),
                FontWeight = FontWeights.UltraBold,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 10)
            };

            TextBlock txtTo = new TextBlock()
            {
                Text = "To: " + To.ToShortDateString(),
                FontSize = 11,
                FontFamily = new FontFamily("Century Gothic"),
                FontWeight = FontWeights.UltraBold,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 10)
            };


            stpHeadText.Children.Add(stpLogo);
            stpHeadText.Children.Add(stpPageName);
            stpHeadText.Children.Add(txtFrom);
            stpHeadText.Children.Add(txtTo);

            blkHeadText.Child = stpHeadText;
        }

        private void Generate_TableTotalSalesText(BlockUIContainer blkTableTotalSalesText)
        {
            //// Main stackPanel in Table Text
            StackPanel stpTableTotalSalesText = new StackPanel();

            // Seperate Line
            var separator1 = new Rectangle();
            separator1.Stroke = new SolidColorBrush(Colors.Black);
            separator1.StrokeThickness = 3;
            separator1.Height = 3;
            separator1.Width = double.NaN;

            //Table Header
            StackPanel stpHeader = new StackPanel();
            TextBlock tbHeader = new TextBlock()
            {
                Text = "Sales Totals",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 13,
                Margin = new Thickness(0, 5, 0, 5),
                FontWeight = FontWeights.Bold
            };
            stpHeader.Children.Add(tbHeader);

            // Seperate Line
            var separator2 = new Rectangle();
            separator2.Stroke = new SolidColorBrush(Colors.Black);
            separator2.StrokeThickness = 3;
            separator2.Height = 3;
            separator2.Width = double.NaN;


            // Calculate Data
            var totalSalesData = CalculateTotalSales();


            // Create Table
            Grid dgDataTable = new Grid();
            dgDataTable.Width = 300;
            // set Columns
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    ColumnDefinition firstCol = new ColumnDefinition();
                    firstCol.Width = new GridLength(180);
                    dgDataTable.ColumnDefinitions.Add(firstCol);
                    continue;
                }
                if (i == 1)
                {
                    ColumnDefinition secondCol = new ColumnDefinition();
                    secondCol.Width = new GridLength(80);
                    dgDataTable.ColumnDefinitions.Add(secondCol);
                    continue;
                }
            }
            // set Rows
            for (int i = 0; i < totalSalesData.Count; i++)
            {
                dgDataTable.RowDefinitions.Add(new RowDefinition());
                foreach (var item in totalSalesData.Values)
                {
                    dgDataTable.RowDefinitions.Add(new RowDefinition());
                }
            }

            // Fill Table data
            int rowIndex = 0;
            foreach (var item in totalSalesData)
            {
                TextBlock txtMeta = new TextBlock()
                {
                    Text = item.Key,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                Grid.SetRow(txtMeta, rowIndex);
                Grid.SetColumn(txtMeta, 0);
                dgDataTable.Children.Add(txtMeta);

                foreach (var keypairvalue in item.Value)
                {
                    StackPanel stpLeftData = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal
                    };
                    TextBlock txtTitle = new TextBlock()
                    {
                        Text = keypairvalue.Title + ": ",
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(10, 0, 0, 0)
                    };
                    TextBlock txtCount = new TextBlock()
                    {
                        Text = keypairvalue.Count.ToString(),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    stpLeftData.Children.Add(txtTitle);
                    stpLeftData.Children.Add(txtCount);
                    Grid.SetRow(stpLeftData, rowIndex + 1);
                    Grid.SetColumn(stpLeftData, 0);
                    dgDataTable.Children.Add(stpLeftData);

                    TextBlock txtAmount = new TextBlock()
                    {
                        Text = string.Format("{0:0.000}", keypairvalue.Amount),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    Grid.SetRow(txtAmount, rowIndex + 1);
                    Grid.SetColumn(txtAmount, 1);
                    dgDataTable.Children.Add(txtAmount);

                    rowIndex++;
                }

                rowIndex++;
            }

            stpTableTotalSalesText.Children.Add(separator1);
            stpTableTotalSalesText.Children.Add(stpHeader);
            stpTableTotalSalesText.Children.Add(separator2);
            stpTableTotalSalesText.Children.Add(dgDataTable);

            blkTableTotalSalesText.Child = stpTableTotalSalesText;
        }

        private void Generate_TablePayAndRefundText(BlockUIContainer blkTablePayAndRefundText)
        {
            //// Main stackPanel in Table Text
            StackPanel stpTablePayAndRefundText = new StackPanel();

            // Seperate Line
            var separator1 = new Rectangle();
            separator1.Stroke = new SolidColorBrush(Colors.Black);
            separator1.StrokeThickness = 3;
            separator1.Height = 3;
            separator1.Width = double.NaN;

            //Table Header
            StackPanel stpHeader = new StackPanel();
            TextBlock tbHeader = new TextBlock()
            {
                Text = "Payment and Refund Totals",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Margin = new Thickness(0, 5, 0, 5),
                FontWeight = FontWeights.Bold
            };
            stpHeader.Children.Add(tbHeader);

            // Seperate Line
            var separator2 = new Rectangle();
            separator2.Stroke = new SolidColorBrush(Colors.Black);
            separator2.StrokeThickness = 3;
            separator2.Height = 3;
            separator2.Width = double.NaN;


            // Calculate Data
            var totalPayAndRefundData = CalculatePayAndRefund();


            // Create Table
            Grid dgDataTable = new Grid();
            dgDataTable.Width = 300;
            // set Columns
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    ColumnDefinition firstCol = new ColumnDefinition();
                    firstCol.Width = new GridLength(180);
                    dgDataTable.ColumnDefinitions.Add(firstCol);
                    continue;
                }
                if (i == 1)
                {
                    ColumnDefinition secondCol = new ColumnDefinition();
                    secondCol.Width = new GridLength(80);
                    dgDataTable.ColumnDefinitions.Add(secondCol);
                    continue;
                }
            }
            // set Rows
            for (int i = 0; i < totalPayAndRefundData.Count; i++)
            {
                dgDataTable.RowDefinitions.Add(new RowDefinition());
                foreach (var item in totalPayAndRefundData.Values)
                {
                    dgDataTable.RowDefinitions.Add(new RowDefinition());
                }
            }

            // Fill Table data
            int rowIndex = 0;
            foreach (var item in totalPayAndRefundData)
            {
                TextBlock txtMeta = new TextBlock()
                {
                    Text = item.Key,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                Grid.SetRow(txtMeta, rowIndex);
                Grid.SetColumn(txtMeta, 0);
                dgDataTable.Children.Add(txtMeta);

                foreach (var keypairvalue in item.Value)
                {
                    StackPanel stpLeftData = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal
                    };
                    TextBlock txtTitle = new TextBlock()
                    {
                        Text = keypairvalue.Title + ": ",
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(10, 0, 0, 0)
                    };
                    TextBlock txtCount = new TextBlock()
                    {
                        Text = keypairvalue.Count.ToString(),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    stpLeftData.Children.Add(txtTitle);
                    stpLeftData.Children.Add(txtCount);
                    Grid.SetRow(stpLeftData, rowIndex + 1);
                    Grid.SetColumn(stpLeftData, 0);
                    dgDataTable.Children.Add(stpLeftData);

                    TextBlock txtAmount = new TextBlock()
                    {
                        Text = string.Format("{0:0.000}", keypairvalue.Amount),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    Grid.SetRow(txtAmount, rowIndex + 1);
                    Grid.SetColumn(txtAmount, 1);
                    dgDataTable.Children.Add(txtAmount);

                    rowIndex++;
                }

                rowIndex++;
            }

            stpTablePayAndRefundText.Children.Add(separator1);
            stpTablePayAndRefundText.Children.Add(stpHeader);
            stpTablePayAndRefundText.Children.Add(separator2);
            stpTablePayAndRefundText.Children.Add(dgDataTable);

            blkTablePayAndRefundText.Child = stpTablePayAndRefundText;
        }

        private void Generate_TableReceiptText(BlockUIContainer blkTableReceiptText)
        {
            //// Main stackPanel in Table Text
            StackPanel stpTableReceiptText = new StackPanel();

            // Seperate Line
            var separator1 = new Rectangle();
            separator1.Stroke = new SolidColorBrush(Colors.Black);
            separator1.StrokeThickness = 3;
            separator1.Height = 3;
            separator1.Width = double.NaN;

            //Table Header
            StackPanel stpHeader = new StackPanel();
            TextBlock tbHeader = new TextBlock()
            {
                Text = "Receipt Totals",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Margin = new Thickness(0, 5, 0, 5),
                FontWeight = FontWeights.Bold
            };
            stpHeader.Children.Add(tbHeader);

            // Seperate Line
            var separator2 = new Rectangle();
            separator2.Stroke = new SolidColorBrush(Colors.Black);
            separator2.StrokeThickness = 3;
            separator2.Height = 3;
            separator2.Width = double.NaN;


            // Calculate Data
            var totalReceiptData = CalculateReceipt();


            // Create Table
            Grid dgDataTable = new Grid();
            dgDataTable.Width = 300;
            // set Columns
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    ColumnDefinition firstCol = new ColumnDefinition();
                    firstCol.Width = new GridLength(180);
                    dgDataTable.ColumnDefinitions.Add(firstCol);
                    continue;
                }
                if (i == 1)
                {
                    ColumnDefinition secondCol = new ColumnDefinition();
                    secondCol.Width = new GridLength(80);
                    dgDataTable.ColumnDefinitions.Add(secondCol);
                    continue;
                }
            }
            // set Rows
            for (int i = 0; i < totalReceiptData.Count; i++)
            {
                dgDataTable.RowDefinitions.Add(new RowDefinition());
                foreach (var item in totalReceiptData.Values)
                {
                    dgDataTable.RowDefinitions.Add(new RowDefinition());
                }
            }

            // Fill Table data
            int rowIndex = 0;
            foreach (var item in totalReceiptData)
            {
                TextBlock txtMeta = new TextBlock()
                {
                    Text = item.Key,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                Grid.SetRow(txtMeta, rowIndex);
                Grid.SetColumn(txtMeta, 0);
                dgDataTable.Children.Add(txtMeta);

                foreach (var keypairvalue in item.Value)
                {
                    StackPanel stpLeftData = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal
                    };
                    TextBlock txtTitle = new TextBlock()
                    {
                        Text = keypairvalue.Title + ": ",
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(10, 0, 0, 0)
                    };
                    TextBlock txtCount = new TextBlock()
                    {
                        Text = keypairvalue.Count.ToString(),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    stpLeftData.Children.Add(txtTitle);
                    stpLeftData.Children.Add(txtCount);
                    Grid.SetRow(stpLeftData, rowIndex + 1);
                    Grid.SetColumn(stpLeftData, 0);
                    dgDataTable.Children.Add(stpLeftData);

                    TextBlock txtAmount = new TextBlock()
                    {
                        Text = string.Format("{0:0.000}", keypairvalue.Amount),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    Grid.SetRow(txtAmount, rowIndex + 1);
                    Grid.SetColumn(txtAmount, 1);
                    dgDataTable.Children.Add(txtAmount);

                    rowIndex++;
                }

                rowIndex++;
            }


            stpTableReceiptText.Children.Add(separator1);
            stpTableReceiptText.Children.Add(stpHeader);
            stpTableReceiptText.Children.Add(separator2);
            stpTableReceiptText.Children.Add(dgDataTable);


            blkTableReceiptText.Child = stpTableReceiptText;
        }



        private Dictionary<string, List<MyPairValue>> CalculateTotalSales()
        {
            var result = new Dictionary<string, List<MyPairValue>>();
            var orderDetailsQuery =
                _cloudPosUnitofwork.OrderDetailsRepository.Get(x => x.OrderNote.Ordertime.CompareTo(From) >= 0
                                                            && x.OrderNote.Ordertime.CompareTo(To) <= 0);
            var orderQuery =
                _cloudPosUnitofwork.OrderRepository.Get(x => x.Ordertime.CompareTo(From) >= 0
                                                     && x.Ordertime.CompareTo(To) <= 0);


            // Total Alcohol
            var orderDetailsAlcoholQuery = orderDetailsQuery.Where(x => x.Product.Type == (int)ProductType.Cocktail
                                                                        || x.Product.Type == (int)ProductType.Beer
                                                                        || x.Product.Type == (int)ProductType.Wine);
            decimal alcoholTotalAmount = 0;
            foreach (var orderDetails in orderDetailsAlcoholQuery)
            {
                alcoholTotalAmount += orderDetails.Quan * (orderDetails.Product.Price * (100-orderDetails.Discount))/100;
            }
            MyPairValue alcoholCal = new MyPairValue()
            {
                Title = "Count",
                Count = orderDetailsAlcoholQuery.Count(),
                Amount = alcoholTotalAmount
            };
            result.Add("Total Alcohol", new List<MyPairValue>()
            {
                alcoholCal
            });


            // Total Beverage
            var orderDetailsBeverageQuery = orderDetailsQuery.Where(x => x.Product.Type == (int)ProductType.Beverage
                                                                        || x.Product.Type == (int)ProductType.Coffee);
            decimal beverageTotalAmount = 0;
            foreach (var orderDetails in orderDetailsBeverageQuery)
            {
                beverageTotalAmount += orderDetails.Quan * (orderDetails.Product.Price * (100 - orderDetails.Discount)) / 100;
            }
            MyPairValue beverageCal = new MyPairValue()
            {
                Title = "Count",
                Count = orderDetailsBeverageQuery.Count(),
                Amount = beverageTotalAmount
            };
            result.Add("Total Beverage", new List<MyPairValue>()
            {
                beverageCal
            });


            // Total Food
            var orderDetailsFoodQuery = orderDetailsQuery.Where(x => x.Product.Type == (int)ProductType.Food);
            decimal foodTotalAmount = 0;
            foreach (var orderDetails in orderDetailsFoodQuery)
            {
                foodTotalAmount += orderDetails.Quan * (orderDetails.Product.Price * (100 - orderDetails.Discount)) / 100;
            }
            MyPairValue foodCal = new MyPairValue()
            {
                Title = "Count",
                Count = orderDetailsFoodQuery.Count(),
                Amount = foodTotalAmount
            };
            result.Add("Total Food", new List<MyPairValue>()
            {
                foodCal
            });


            // Total Other
            var orderDetailsOtherQuery = orderDetailsQuery.Where(x => x.Product.Type == (int)ProductType.Other);
            decimal otherTotalAmount = 0;
            foreach (var orderDetails in orderDetailsOtherQuery)
            {
                otherTotalAmount += orderDetails.Quan * (orderDetails.Product.Price * (100 - orderDetails.Discount)) / 100;
            }
            MyPairValue otherCal = new MyPairValue()
            {
                Title = "Count",
                Count = orderDetailsOtherQuery.Count(),
                Amount = otherTotalAmount
            };
            result.Add("Total Other", new List<MyPairValue>()
            {
                otherCal
            });


            // SubTotal
            // real TotalAmount
            decimal totalAmount = 0;
            foreach (var orderDetails in orderDetailsQuery)
            {
                totalAmount += orderDetails.Quan * (orderDetails.Product.Price * (100-orderDetails.Discount))/100;
            }
            MyPairValue orderTotalCal = new MyPairValue()
            {
                Title = "Orders",
                Amount = totalAmount,
                Count = orderQuery.Count()
            };

            // SVC
            decimal totalSVC = 0;
            foreach (var order in orderQuery)
            {
                decimal curTotalAmount = 0;
                foreach (var orderDetails in order.OrderNoteDetails)
                {
                    curTotalAmount += orderDetails.Quan * (orderDetails.Product.Price * (100 - orderDetails.Discount)) / 100;
                }

                totalSVC += (curTotalAmount * 5) / 100;
            }
            MyPairValue SVCTotalCal = new MyPairValue()
            {
                Title = "Service Charge",
                Amount = totalSVC,
                Count = orderQuery.Count()
            };

            // VAT
            decimal totalVAT = 0;
            foreach (var order in orderQuery)
            {
                decimal curTotalAmount = 0;
                foreach (var orderDetails in order.OrderNoteDetails)
                {
                    curTotalAmount += orderDetails.Quan * (orderDetails.Product.Price * (100 - orderDetails.Discount)) / 100;
                }

                totalVAT += ((((curTotalAmount * 5)/100) + curTotalAmount) * 10) / 100;
            }
            MyPairValue VATTotalCal = new MyPairValue()
            {
                Title = "VAT",
                Amount = totalVAT,
                Count = orderQuery.Count()
            };

            // DIscount
            decimal totalDisc = 0;
            int countDisc = 0;
            foreach (var order in orderQuery)
            {
                if (order.Discount != 0)
                {
                    totalDisc += order.TotalPriceNonDisc - order.TotalPrice;
                    countDisc++;
                }
            }
            MyPairValue DiscTotalCal = new MyPairValue()
            {
                Title = "Discount",
                Amount = totalDisc,
                Count = countDisc
            };

            result.Add("SubTotal", new List<MyPairValue>()
            {
                orderTotalCal,
                SVCTotalCal,
                VATTotalCal,
                DiscTotalCal
            });


            // Total
            decimal total = 0;
            foreach (var order in orderQuery)
            {
                total += order.TotalPrice;
            }
            MyPairValue totalCal = new MyPairValue()
            {
                Title = "Orders",
                Amount = total,
                Count = orderQuery.Count()
            };
            result.Add("Total", new List<MyPairValue>()
            {
                totalCal
            });

            return result;
        }

        private Dictionary<string, List<MyPairValue>> CalculatePayAndRefund()
        {
            var result = new Dictionary<string, List<MyPairValue>>();
            var orderQuery =
                _cloudPosUnitofwork.OrderRepository.Get(x => x.Ordertime.CompareTo(From) >= 0
                                                     && x.Ordertime.CompareTo(To) <= 0);

            //Total Cash
            var orderCashQuery = orderQuery.Where(x => x.paymentMethod == (int)PaymentMethod.Cash);
            decimal cashTotalAmount = 0;
            foreach (var order in orderCashQuery)
            {
                cashTotalAmount += order.TotalPrice;
            }
            MyPairValue cashCal = new MyPairValue()
            {
                Title = "Orders",
                Count = orderCashQuery.Count(),
                Amount = cashTotalAmount
            };
            result.Add("Cash", new List<MyPairValue>()
            {
                cashCal
            });


            //Total Cheque
            var orderChequeQuery = orderQuery.Where(x => x.paymentMethod == (int)PaymentMethod.Cheque);
            decimal chequeTotalAmount = 0;
            foreach (var order in orderChequeQuery)
            {
                chequeTotalAmount += order.TotalPrice;
            }
            MyPairValue chequeCal = new MyPairValue()
            {
                Title = "Orders",
                Count = orderChequeQuery.Count(),
                Amount = chequeTotalAmount
            };
            result.Add("Cheque", new List<MyPairValue>()
            {
                chequeCal
            });


            //Total Deferred
            var orderDeferredQuery = orderQuery.Where(x => x.paymentMethod == (int)PaymentMethod.Deferred);
            decimal deferredTotalAmount = 0;
            foreach (var order in orderDeferredQuery)
            {
                deferredTotalAmount += order.TotalPrice;
            }
            MyPairValue defferedCal = new MyPairValue()
            {
                Title = "Orders",
                Count = orderDeferredQuery.Count(),
                Amount = deferredTotalAmount
            };
            result.Add("Deferred", new List<MyPairValue>()
            {
                defferedCal
            });


            //Total International(Visa)
            var orderInterQuery = orderQuery.Where(x => x.paymentMethod == (int)PaymentMethod.International);
            decimal interTotalAmount = 0;
            foreach (var order in orderInterQuery)
            {
                interTotalAmount += order.TotalPrice;
            }
            MyPairValue interCal = new MyPairValue()
            {
                Title = "Orders",
                Count = orderInterQuery.Count(),
                Amount = interTotalAmount
            };
            result.Add("International(Visa)", new List<MyPairValue>()
            {
                interCal
            });


            //Total Credit
            var orderCreditQuery = orderQuery.Where(x => x.paymentMethod == (int)PaymentMethod.Credit);
            decimal creditTotalAmount = 0;
            foreach (var order in orderCreditQuery)
            {
                creditTotalAmount += order.TotalPrice;
            }
            MyPairValue creditCal = new MyPairValue()
            {
                Title = "Orders",
                Count = orderCreditQuery.Count(),
                Amount = creditTotalAmount
            };
            result.Add("Credit", new List<MyPairValue>()
            {
                creditCal
            });


            //Total OnAcount
            var orderOnAcountQuery = orderQuery.Where(x => x.paymentMethod == (int)PaymentMethod.OnAcount);
            decimal onAcountTotalAmount = 0;
            foreach (var order in orderOnAcountQuery)
            {
                onAcountTotalAmount += order.TotalPrice;
            }
            MyPairValue onAcountCal = new MyPairValue()
            {
                Title = "Orders",
                Count = orderOnAcountQuery.Count(),
                Amount = onAcountTotalAmount
            };
            result.Add("On Account", new List<MyPairValue>()
            {
                onAcountCal
            });


            return result;
        }

        private Dictionary<string, List<MyPairValue>> CalculateReceipt()
        {
            var result = new Dictionary<string, List<MyPairValue>>();

            var receiptQuery =
                _cloudPosUnitofwork.ReceiptNoteRepository.Get(x => x.Inday.CompareTo(From) >= 0
                                                     && x.Inday.CompareTo(To) <= 0);

            //Total Receipt
            decimal totalAmount = 0;
            foreach (var order in receiptQuery)
            {
                totalAmount += order.TotalAmount;
            }
            MyPairValue receiptCal = new MyPairValue()
            {
                Title = "Bills",
                Count = receiptQuery.Count(),
                Amount = totalAmount
            };
            result.Add("Total", new List<MyPairValue>()
            {
                receiptCal
            });

            return result;
        }
    }

    public class MyPairValue
    {
        public string Title { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
    }
}
