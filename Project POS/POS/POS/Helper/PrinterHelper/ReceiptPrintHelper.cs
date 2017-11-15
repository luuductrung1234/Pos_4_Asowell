using POS.Helper.PrintHelper.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace POS.Helper.PrintHelper
{
    public class ReceiptPrintHelper : IPrintHelper
    {
        private static string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        
        public FlowDocument CreateDocument()
        {
            return CreateReceiptDocument();
        }


        /// <summary>
        /// Create a complete Receipt
        /// </summary>
        /// <returns></returns>
        public FlowDocument CreateReceiptDocument()
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


            // Template Data for print
            OrderForPrint order = new OrderForPrint()
            {
                No = "ORD0000001",
                Table = 1,
                Date = DateTime.Now,
                Casher = "Luong Nhat Duy",
                Customer = "Luu Duc Trung",
                CustomerPay = 500
            };


            // Head Text
            BlockUIContainer blkHeadText = new BlockUIContainer();
            Generate_HeadText(blkHeadText);

            // Info Text
            BlockUIContainer blkInfoText = new BlockUIContainer();
            Generate_InfoText(blkInfoText, order.getMetaReceiptInfo());

            // Table Text
            BlockUIContainer blkTableText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_TableText(blkTableText, order.getMetaReceiptTable(), order.OrderDetails);

            // Summary Text
            BlockUIContainer blkSummaryText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_SummaryText(blkSummaryText, order, "vnd");


            //// Add Paragraph to Section
            //sec.Blocks.Add(p1);
            sec.Blocks.Add(blkHeadText);
            sec.Blocks.Add(blkInfoText);
            sec.Blocks.Add(blkTableText);
            sec.Blocks.Add(blkSummaryText);

            // Add Section to FlowDocument
            doc.Blocks.Add(sec);


            return doc;
        }


        /// <summary>
        /// Create the summary section of Receipt
        /// </summary>
        /// <param name="blkTableText"></param>
        /// <param name="order">the order data for filling the receip</param>
        /// <param name="moneyUnit"></param>
        private void Generate_SummaryText(BlockUIContainer blkSummaryText, OrderForPrint order, string moneyUnit)
        {
            StackPanel stpSummary = new StackPanel()
            {
                Orientation = Orientation.Vertical
            };


            // Total Price
            StackPanel stpTotalPrice = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock tbTotalPriceLable = new TextBlock()
            {
                Text = "Total Amount:" + "(" + moneyUnit + ")",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 12,
                FontWeight = FontWeights.UltraBold,
                Margin = new Thickness(110, 0, 10, 0),
                Width = 110
            };
            TextBlock tbTotalPriceValue = new TextBlock()
            {
                Text = String.Format("{0:0.000}", order.TotalPrice),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 11,
                Width = 100
            };
            stpTotalPrice.Children.Add(tbTotalPriceLable);
            stpTotalPrice.Children.Add(tbTotalPriceValue);

            // Customer Pay
            StackPanel stpCustomerPay = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock tbCustomerPayLable = new TextBlock()
            {
                Text = "Customer Pay:",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 12,
                FontWeight = FontWeights.UltraBold,
                Margin = new Thickness(110, 0, 10, 0),
                Width = 110
            };
            TextBlock tbCustomerPayValue = new TextBlock()
            {
                Text = String.Format("{0:0.000}", order.TotalPrice),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 11,
                Width = 100
            };
            stpCustomerPay.Children.Add(tbCustomerPayLable);
            stpCustomerPay.Children.Add(tbCustomerPayValue);

            // Pay Back
            StackPanel stpPayBack = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock tbPayBackLable = new TextBlock()
            {
                Text = "Change:",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 12,
                FontWeight = FontWeights.UltraBold,
                Margin = new Thickness(110, 0, 10, 0),
                Width = 110
            };
            TextBlock tbPayBackValue = new TextBlock()
            {
                Text = String.Format("{0:0.000}", order.PayBack),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 11,
                Width = 100
            };
            stpPayBack.Children.Add(tbPayBackLable);
            stpPayBack.Children.Add(tbPayBackValue);



            stpSummary.Children.Add(stpTotalPrice);
            stpSummary.Children.Add(stpCustomerPay);
            stpSummary.Children.Add(stpPayBack);


            blkSummaryText.Child = stpSummary;
        }

        /// <summary>
        /// Create the table section of Receipt
        /// </summary>
        /// <param name="blkTableText"></param>
        /// <param name="gridMeta">The meta header of the table</param>
        /// <param name="listData">The data source for table</param>
        private void Generate_TableText(BlockUIContainer blkTableText, string[] gridMeta, List<OrderDetailsForPrint> listData)
        {
            Grid dgDataTable = new Grid();
            dgDataTable.Width = 300;
            // set Columns
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    ColumnDefinition firstCol = new ColumnDefinition();
                    firstCol.Width = new GridLength(120);
                    dgDataTable.ColumnDefinitions.Add(firstCol);
                    continue;
                }
                if (i == 1)
                {
                    ColumnDefinition secondCol = new ColumnDefinition();
                    secondCol.Width = new GridLength(40);
                    dgDataTable.ColumnDefinitions.Add(secondCol);
                    continue;
                }
                else
                {
                    ColumnDefinition otherCol = new ColumnDefinition();
                    otherCol.Width = new GridLength(70);
                    dgDataTable.ColumnDefinitions.Add(otherCol);
                }
            }
            // set Rows
            for (int i = 0; i < listData.Count + 1; i++)
            {
                dgDataTable.RowDefinitions.Add(new RowDefinition());
            }


            // add Meta
            for (int i = 0; i < 4; i++)
            {
                TextBlock txtMeta = new TextBlock();
                txtMeta.Text = gridMeta[i];
                txtMeta.FontSize = 11;
                txtMeta.FontWeight = FontWeights.Bold;
                txtMeta.VerticalAlignment = VerticalAlignment.Top;
                Grid.SetRow(txtMeta, 0);
                Grid.SetColumn(txtMeta, i);

                dgDataTable.Children.Add(txtMeta);
            }

            int rowIndex = 1;
            foreach (var orderItem in listData)
            {
                TextBlock txtProductName = new TextBlock();
                txtProductName.Width = 115;
                txtProductName.Text = orderItem.ProductName;
                txtProductName.FontSize = 11;
                txtProductName.VerticalAlignment = VerticalAlignment.Top;
                txtProductName.HorizontalAlignment = HorizontalAlignment.Left;
                Grid.SetRow(txtProductName, rowIndex);
                Grid.SetColumn(txtProductName, 0);
                dgDataTable.Children.Add(txtProductName);

                TextBlock txtQuan = new TextBlock();
                txtQuan.Text = orderItem.Quan.ToString();
                txtQuan.FontSize = 11;
                txtQuan.VerticalAlignment = VerticalAlignment.Top;
                Grid.SetRow(txtQuan, rowIndex);
                Grid.SetColumn(txtQuan, 1);
                dgDataTable.Children.Add(txtQuan);

                TextBlock txtPrice = new TextBlock();
                txtPrice.Text = String.Format("{0:0.000}", orderItem.ProductPrice);
                txtPrice.FontSize = 11;
                txtPrice.VerticalAlignment = VerticalAlignment.Top;
                txtPrice.HorizontalAlignment = HorizontalAlignment.Left;
                Grid.SetRow(txtPrice, rowIndex);
                Grid.SetColumn(txtPrice, 2);
                dgDataTable.Children.Add(txtPrice);

                TextBlock txtAmt = new TextBlock();
                txtAmt.Text = String.Format("{0:0.000}", orderItem.Amt);
                txtAmt.FontSize = 11;
                txtAmt.VerticalAlignment = VerticalAlignment.Top;
                txtAmt.TextAlignment = TextAlignment.Left;
                Grid.SetRow(txtAmt, rowIndex);
                Grid.SetColumn(txtAmt, 3);
                dgDataTable.Children.Add(txtAmt);

                rowIndex++;
            }

            blkTableText.Child = dgDataTable;
        }

        /// <summary>
        /// Create the Info section of Receipt
        /// </summary>
        /// <param name="blkInfoText"></param>
        /// <param name="infos">The information of Receipt</param>
        private void Generate_InfoText(BlockUIContainer blkInfoText, Dictionary<string, string> infos)
        {
            StackPanel stpMain = new StackPanel();

            foreach (var info in infos)
            {
                StackPanel stpInfo = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };
                TextBlock tbInfoKey = new TextBlock()
                {
                    Text = info.Key + ":",
                    FontFamily = new FontFamily("Century Gothic"),
                    FontSize = 10,
                    FontWeight = FontWeights.UltraBold,
                    Margin = new Thickness(0, 0, 10, 0),
                    Width = 70
                };
                TextBlock tbInfoValue = new TextBlock()
                {
                    Text = info.Value,
                    FontFamily = new FontFamily("Century Gothic"),
                    FontSize = 10,
                    Width = 150
                };
                stpInfo.Children.Add(tbInfoKey);
                stpInfo.Children.Add(tbInfoValue);

                stpMain.Children.Add(stpInfo);
            }

            blkInfoText.Child = stpMain;
        }

        /// <summary>
        /// Create the Head section of Receipt
        /// </summary>
        /// <param name="blkHeadText"></param>
        private void Generate_HeadText(BlockUIContainer blkHeadText)
        {

            StackPanel stpHeadText = new StackPanel()
            {
                Orientation = Orientation.Vertical
            };

            StackPanel stpLogo = new StackPanel();
            Image imgOwner = new Image();
            BitmapImage bimg = new BitmapImage();
            bimg.BeginInit();
            bimg.UriSource = new Uri(startupProjectPath + "\\image\\logo.png", UriKind.Absolute);
            bimg.EndInit();
            imgOwner.Source = bimg;
            imgOwner.HorizontalAlignment = HorizontalAlignment.Center;
            imgOwner.Margin = new Thickness(85, 0, 0, 0);
            stpLogo.Children.Add(imgOwner);

            //TextBlock txtOwnerName = new TextBlock()
            //{
            //    Text = "Asowell Restaurant",
            //    FontSize = 12,
            //    FontFamily = new FontFamily("Century Gothic"),
            //    FontWeight = FontWeights.UltraBold
            //};
            TextBlock txtAddress1 = new TextBlock()
            {
                Text = "Address: f.7th, Fafilm Building, 6 St.Thai Van Lung, w.Ben Nghe, Dist 1, HCM City",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 10
            };
            TextBlock txtAddress2 = new TextBlock()
            {
                Text = "Address: f.7th, Fafilm Building, 6 St.Thai Van Lung, w.Ben Nghe, Dist 1, HCM City",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 10
            };
            TextBlock txtPhone = new TextBlock()
            {
                Text = "Phone: 0927333668",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 10,
                Margin = new Thickness(0, 0, 0, 5)
            };
            StackPanel stpPageName = new StackPanel();
            TextBlock txtPageName = new TextBlock()
            {
                Text = "RECEIPT",
                FontSize = 13,
                FontFamily = new FontFamily("Century Gothic"),
                FontWeight = FontWeights.UltraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            stpPageName.Children.Add(txtPageName);


            stpHeadText.Children.Add(stpLogo);
            //stpHeadText.Children.Add(txtOwnerName);
            stpHeadText.Children.Add(txtAddress1);
            stpHeadText.Children.Add(txtAddress2);
            stpHeadText.Children.Add(txtPhone);
            stpHeadText.Children.Add(stpPageName);

            blkHeadText.Child = stpHeadText;
        }
    }
}
