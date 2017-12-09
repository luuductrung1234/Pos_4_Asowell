using POS.Helper.PrintHelper.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace POS.Helper.PrintHelper
{
    public class KitchenPrintHelper : IPrintHelper
    {
        private static string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        public OrderForPrint Order { get; set; }

        public FlowDocument CreateDocument()
        {
            return CreateKitchenDocument();
        }

        public FlowDocument CreateKitchenDocument()
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
            Generate_HeadText(blkHeadText, Order);


            // Table Text
            BlockUIContainer blkTableText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_TableText(blkTableText, Order.getMetaKitchenTable(), Order.GetOrderDetailsForKitchen());



            //// Add Paragraph to Section
            //sec.Blocks.Add(p1);
            sec.Blocks.Add(blkHeadText);
            sec.Blocks.Add(blkTableText);

            // Add Section to FlowDocument
            doc.Blocks.Add(sec);


            return doc;
        }


        /// <summary>
        /// Create the Head Section in Kitchen Print
        /// </summary>
        /// <param name="blkHeadText"></param>
        /// <param name="order"></param>
        private void Generate_HeadText(BlockUIContainer blkHeadText, OrderForPrint order)
        {
            // Main stackPanel in Head Text
            StackPanel stpHeadText = new StackPanel();

            StackPanel stpTime = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBlock tbDate = new TextBlock()
            {
                Text = order.Date.ToShortDateString(),
                Width = 120,
                VerticalAlignment = VerticalAlignment.Stretch,
                TextAlignment = TextAlignment.Left,
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 18,
                FontWeight = FontWeights.UltraBold,
            };
            TextBlock tbTime = new TextBlock()
            {
                Text = order.Date.ToShortTimeString(),
                Width = 150,
                VerticalAlignment = VerticalAlignment.Stretch,
                TextAlignment = TextAlignment.Right,
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 18,
                FontWeight = FontWeights.UltraBold,
            };
            stpTime.Children.Add(tbDate);
            stpTime.Children.Add(tbTime);


            StackPanel stpTableNumber = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock tbTableNumberLabel = new TextBlock()
            {
                Text = "Table:    ",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 18,
                FontWeight = FontWeights.UltraBold,
            };
            TextBlock tbTableNumber = new TextBlock()
            {
                Text = order.Table.ToString(),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 18,
                Width = 30,
                FontWeight = FontWeights.UltraBold,
            };
            TextBlock tbPaxLable = new TextBlock()
            {
                Text = "Pax:    ",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 18,
                Width = 40,
                Margin = new Thickness(95, 0, 0, 0),
                FontWeight = FontWeights.UltraBold,
            };
            TextBlock tbPaxValue = new TextBlock()
            {
                Text = order.Pax.ToString(),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 18,
                Width = 35,
                TextAlignment = TextAlignment.Right,
                FontWeight = FontWeights.UltraBold,
            };
            stpTableNumber.Children.Add(tbTableNumberLabel);
            stpTableNumber.Children.Add(tbTableNumber);
            stpTableNumber.Children.Add(tbPaxLable);
            stpTableNumber.Children.Add(tbPaxValue);


            StackPanel stpPageName = new StackPanel();
            TextBlock tbPageName = new TextBlock()
            {
                Text = "KITCHEN",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 20,
                Margin = new Thickness(0, 10, 0, 0),
                FontFamily = new FontFamily("Century Gothic"),
                FontWeight = FontWeights.UltraBold,
            };
            stpPageName.Children.Add(tbPageName);

            stpHeadText.Children.Add(stpTime);
            stpHeadText.Children.Add(stpTableNumber);
            stpHeadText.Children.Add(stpPageName);

            blkHeadText.Child = stpHeadText;
        }

        /// <summary>
        /// Create the Table Section in Kitchen Print
        /// </summary>
        /// <param name="blkHeadText"></param>
        /// <param name="order"></param>
        public void Generate_TableText(BlockUIContainer blkTableText, string[] gridMeta, Dictionary<string, Dictionary<int, List<OrderDetailsForPrint>>> listData)
        {
            // Main stackPanel in Table Text
            StackPanel stpTableText = new StackPanel()
            {
                Margin = new Thickness(0,0,0,10)
            };

            bool isShowMeta = false;
            // Data for each Product Status
            foreach (var dataInStatus in listData)
            {

                Grid dgDataTable = new Grid();
                dgDataTable.Width = 300;
                // set Columns
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        ColumnDefinition firstCol = new ColumnDefinition();
                        firstCol.Width = new GridLength(75);
                        dgDataTable.ColumnDefinitions.Add(firstCol);
                        continue;
                    }
                    if (i == 1)
                    {
                        ColumnDefinition secondCol = new ColumnDefinition();
                        secondCol.Width = new GridLength(50);
                        dgDataTable.ColumnDefinitions.Add(secondCol);
                        continue;
                    }
                    if (i == 2)
                    {
                        ColumnDefinition otherCol = new ColumnDefinition();
                        otherCol.Width = new GridLength(160);
                        dgDataTable.ColumnDefinitions.Add(otherCol);
                        continue;
                    }
                }
                // set Rows
                dgDataTable.RowDefinitions.Add(new RowDefinition());    // extra row for meta
                foreach (var orderDetailInChair in dataInStatus.Value.Values)
                {
                    foreach (var orderDetails in orderDetailInChair)
                    {
                        dgDataTable.RowDefinitions.Add(new RowDefinition());
                    }
                }


                // add Meta
                if (!isShowMeta)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        TextBlock txtMeta = new TextBlock();
                        txtMeta.Text = gridMeta[i];
                        txtMeta.FontSize = 15;
                        txtMeta.FontWeight = FontWeights.Bold;
                        txtMeta.VerticalAlignment = VerticalAlignment.Stretch;
                        Grid.SetRow(txtMeta, 0);
                        Grid.SetColumn(txtMeta, i);

                        dgDataTable.Children.Add(txtMeta);
                    }
                }
                isShowMeta = true;


                // Status
                StackPanel stpStatus = new StackPanel();
                TextBlock tbStatus = new TextBlock()
                {
                    Text = dataInStatus.Key,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 18,
                    FontWeight = FontWeights.Bold
                };
                stpStatus.Children.Add(tbStatus);

                
                // Data
                int bigRowIndex = 1;
                foreach (var chairInStatus in dataInStatus.Value)
                {
                    if (chairInStatus.Value.Count == 0)
                    {
                        continue;
                    }

                    // CHAIR
                    StackPanel stpChair = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal
                    };
                    TextBlock tbChairNumber = new TextBlock()
                    {
                        Text = chairInStatus.Key.ToString(),
                        FontFamily = new FontFamily("Century Gothic"),
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(0,0,10,0)
                    };
                    stpChair.Children.Add(tbChairNumber);
                    Grid.SetRow(stpChair, bigRowIndex);
                    Grid.SetColumn(stpChair, 0);
                    Grid.SetRowSpan(stpChair, chairInStatus.Value.Count);
                    dgDataTable.Children.Add(stpChair);

                    int smallRowIndex = bigRowIndex;
                    foreach (var orderDetailsInChair in chairInStatus.Value)
                    {
                        // QUANTITY
                        TextBlock tbQuan = new TextBlock()
                        {
                            Text = orderDetailsInChair.Quan.ToString(),
                            FontSize = 16,
                            FontFamily = new FontFamily("Century Gothic"),
                        };
                        Grid.SetRow(tbQuan, smallRowIndex);
                        Grid.SetColumn(tbQuan, 1);
                        dgDataTable.Children.Add(tbQuan);

                        // INFO
                        StackPanel stpInfo = new StackPanel()
                        {
                            Orientation = Orientation.Vertical
                        };
                        TextBlock tbProductName = new TextBlock()
                        {
                            Text = "- " + seperateLongProductName(orderDetailsInChair.ProductName),
                            FontSize = 16,
                            FontFamily = new FontFamily("Century Gothic"),
                        };
                        TextBlock tbNote = new TextBlock()
                        {
                            Inlines = { new Italic(new Run(orderDetailsInChair.Note)) },
                            FontSize = 12
                        };
                        stpInfo.Children.Add(tbProductName);
                        stpInfo.Children.Add(tbNote);
                        Grid.SetRow(stpInfo, smallRowIndex);
                        Grid.SetColumn(stpInfo, 2);
                        dgDataTable.Children.Add(stpInfo);

                        smallRowIndex++;
                    }


                    bigRowIndex += chairInStatus.Value.Count;
                }


                // Seperate Line
                var separator = new Rectangle();
                separator.Stroke = new SolidColorBrush(Colors.Black);
                separator.StrokeThickness = 3;
                separator.Height = 3;
                separator.Width = double.NaN;

                stpTableText.Children.Add(stpStatus);
                stpTableText.Children.Add(dgDataTable);
                stpTableText.Children.Add(separator);

            }


            blkTableText.Child = stpTableText;
        }


        private string seperateLongProductName(string productName)
        {
            string result = "";

            string[] splProductName = productName.Split(' ');
            result = splProductName[0];

            string line = result;
            for (int i = 1; i < splProductName.Length; i++)
            {
                line += " " + splProductName[i];
                if (line.Length > 16)
                {
                    result += "\n" + splProductName[i];
                    line = splProductName[i];
                }
                else
                {
                    result += " " + splProductName[i];
                }
            }

            return result;
        }
    }
}
