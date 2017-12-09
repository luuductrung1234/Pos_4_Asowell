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

        public Owner Owner { get; set; }

        public OrderForPrint Order { get; set; }

        public ReceiptPrintHelper() { }

        public ReceiptPrintHelper(Owner owner)
        {
            this.Owner = owner;
        }

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


            // Head Text
            BlockUIContainer blkHeadText = new BlockUIContainer();
            if (Owner != null)
            {
                Generate_HeadText(blkHeadText, Owner);
            }
            

            // Info Text
            BlockUIContainer blkInfoText = new BlockUIContainer();
            Generate_InfoText(blkInfoText, Order.getMetaReceiptInfo());

            // Table Text
            BlockUIContainer blkTableText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_TableText(blkTableText, Order.getMetaReceiptTable(), Order.GetOrderDetailsForReceipt());

            // Summary Text
            BlockUIContainer blkSummaryText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_SummaryText(blkSummaryText, Order, "vnd");


            // Food Text
            BlockUIContainer blkFootText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 20, 0, 0)
            };
            Generate_FootText(blkFootText);

            //// Add Paragraph to Section
            //sec.Blocks.Add(p1);
            sec.Blocks.Add(blkHeadText);
            sec.Blocks.Add(blkInfoText);
            sec.Blocks.Add(blkTableText);
            sec.Blocks.Add(blkSummaryText);
            sec.Blocks.Add(blkFootText);

            // Add Section to FlowDocument
            doc.Blocks.Add(sec);


            return doc;
        }



        /// <summary>
        /// Create the Foot Section of Receipt
        /// </summary>
        /// <param name="blkFootText"></param>
        private void Generate_FootText(BlockUIContainer blkFootText)
        {
            // Main stackPanel of Foot Text
            StackPanel stpFootText = new StackPanel();

            StackPanel stpThank = new StackPanel();
            TextBlock txtThank = new TextBlock()
            {
                Text = "Thank you!",
                FontSize = 11,
                FontFamily = new FontFamily("Century Gothic"),
                FontWeight = FontWeights.UltraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
            };
            stpThank.Children.Add(txtThank);

            StackPanel stpSeeAgain = new StackPanel();
            TextBlock txtSeeAgain = new TextBlock()
            {
                Text = "See You Again!",
                FontSize = 11,
                FontFamily = new FontFamily("Century Gothic"),
                FontWeight = FontWeights.UltraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0)
            };
            stpSeeAgain.Children.Add(txtSeeAgain);

            Random rand = new Random();
            StackPanel stpEmo = new StackPanel();
            Image imgEmo = new Image();
            BitmapImage bimg = new BitmapImage();
            bimg.BeginInit();
            bimg.UriSource = new Uri(startupProjectPath + "\\Images\\rand" + rand.Next(5) + ".png", UriKind.Absolute);
            bimg.EndInit();
            imgEmo.Source = bimg;
            imgEmo.HorizontalAlignment = HorizontalAlignment.Center;
            imgEmo.Margin = new Thickness(125, 0, 0, 0);
            stpEmo.Children.Add(imgEmo);


            stpFootText.Children.Add(stpThank);
            stpFootText.Children.Add(stpSeeAgain);
            stpFootText.Children.Add(stpEmo);

            blkFootText.Child = stpFootText;
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


            // Sale Value
            StackPanel stpSaleValue = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock tbSaleValueLable = new TextBlock()
            {
                Text = "Sale Value:",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 12,
                FontWeight = FontWeights.UltraBold,
                Margin = new Thickness(90, 0, 0, 0),
                Width = 120
            };
            TextBlock tbSaleValueValue = new TextBlock()
            {
                Text = String.Format("{0:0.000}", order.SaleValue),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 11,
                Width = 60,
                TextAlignment = TextAlignment.Right
            };
            stpSaleValue.Children.Add(tbSaleValueLable);
            stpSaleValue.Children.Add(tbSaleValueValue);


            // Service Charge
            StackPanel stpSVC = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock tbSVCLable = new TextBlock()
            {
                Text = "SVC (5%):",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 12,
                FontWeight = FontWeights.UltraBold,
                Margin = new Thickness(90, 0, 0, 0),
                Width = 120
            };
            TextBlock tbSVCValue = new TextBlock()
            {
                Text = String.Format("{0:0.000}", order.Svc),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 11,
                Width = 60,
                TextAlignment = TextAlignment.Right
            };
            stpSVC.Children.Add(tbSVCLable);
            stpSVC.Children.Add(tbSVCValue);


            // VAT
            StackPanel stpVAT = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock tbVATLable = new TextBlock()
            {
                Text = "VAT (10%):",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 12,
                FontWeight = FontWeights.UltraBold,
                Margin = new Thickness(90, 0, 0, 0),
                Width = 120
            };
            TextBlock tbVATValue = new TextBlock()
            {
                Text = String.Format("{0:0.000}", order.Vat),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 11,
                Width = 60,
                TextAlignment = TextAlignment.Right
            };
            stpVAT.Children.Add(tbVATLable);
            stpVAT.Children.Add(tbVATValue);
            


            // Total Price
            StackPanel stpTotalPrice = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock tbTotalPriceLable = new TextBlock()
            {
                Text = "Total Amount " + "(" + moneyUnit + "):",
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 12,
                FontWeight = FontWeights.UltraBold,
                Margin = new Thickness(90, 0, 0, 0),
                Width = 120
            };
            TextBlock tbTotalPriceValue = new TextBlock()
            {
                Text = String.Format("{0:0.000}", order.TotalPrice),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 11,
                Width = 60,
                TextAlignment = TextAlignment.Right
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
                Margin = new Thickness(90, 0, 0, 0),
                Width = 120
            };
            TextBlock tbCustomerPayValue = new TextBlock()
            {
                Text = String.Format("{0:0.000}", order.CustomerPay),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 11,
                Width = 60,
                TextAlignment = TextAlignment.Right
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
                Margin = new Thickness(90, 0, 0, 0),
                Width = 120
            };
            TextBlock tbPayBackValue = new TextBlock()
            {
                Text = String.Format("{0:0.000}", order.PayBack),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 11,
                Width = 60,
                TextAlignment = TextAlignment.Right
            };
            stpPayBack.Children.Add(tbPayBackLable);
            stpPayBack.Children.Add(tbPayBackValue);


            stpSummary.Children.Add(stpSaleValue);
            stpSummary.Children.Add(stpSVC);
            stpSummary.Children.Add(stpVAT);
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
                    secondCol.Width = new GridLength(30);
                    dgDataTable.ColumnDefinitions.Add(secondCol);
                    continue;
                }
                if (i == 2)
                {
                    ColumnDefinition otherCol = new ColumnDefinition();
                    otherCol.Width = new GridLength(55);
                    dgDataTable.ColumnDefinitions.Add(otherCol);
                    continue;
                }
                else
                {
                    ColumnDefinition otherCol = new ColumnDefinition();
                    otherCol.Width = new GridLength(65);
                    dgDataTable.ColumnDefinitions.Add(otherCol);
                    continue;
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
                if (i == 2 || i == 3)
                {
                    txtMeta.Text = gridMeta[i];
                    txtMeta.FontSize = 11;
                    txtMeta.FontWeight = FontWeights.Bold;
                    txtMeta.VerticalAlignment = VerticalAlignment.Stretch;
                    txtMeta.TextAlignment = TextAlignment.Right;
                    Grid.SetRow(txtMeta, 0);
                    Grid.SetColumn(txtMeta, i);
                }
                else
                {
                    txtMeta.Text = gridMeta[i];
                    txtMeta.FontSize = 11;
                    txtMeta.FontWeight = FontWeights.Bold;
                    txtMeta.VerticalAlignment = VerticalAlignment.Stretch;
                    Grid.SetRow(txtMeta, 0);
                    Grid.SetColumn(txtMeta, i);
                }

                dgDataTable.Children.Add(txtMeta);
            }

            int rowIndex = 1;
            foreach (var orderItem in listData)
            {
                TextBlock txtProductName = new TextBlock();
                txtProductName.Width = 115;
                txtProductName.Text = seperateLongProductName(orderItem.ProductName);
                txtProductName.FontSize = 11;
                txtProductName.VerticalAlignment = VerticalAlignment.Top;
                txtProductName.HorizontalAlignment = HorizontalAlignment.Left;
                txtProductName.Margin = new Thickness(0,0,0,5);
                Grid.SetRow(txtProductName, rowIndex);
                Grid.SetColumn(txtProductName, 0);
                dgDataTable.Children.Add(txtProductName);

                TextBlock txtQuan = new TextBlock();
                txtQuan.Text = orderItem.Quan.ToString();
                txtQuan.FontSize = 11;
                txtQuan.VerticalAlignment = VerticalAlignment.Top;
                txtQuan.Margin = new Thickness(0, 0, 0, 5);
                Grid.SetRow(txtQuan, rowIndex);
                Grid.SetColumn(txtQuan, 1);
                dgDataTable.Children.Add(txtQuan);

                TextBlock txtPrice = new TextBlock();
                txtPrice.Text = String.Format("{0:0.000}", orderItem.ProductPrice);
                txtPrice.FontSize = 11;
                txtPrice.VerticalAlignment = VerticalAlignment.Stretch;
                txtPrice.HorizontalAlignment = HorizontalAlignment.Right;
                txtPrice.Margin = new Thickness(0, 0, 0, 5);
                Grid.SetRow(txtPrice, rowIndex);
                Grid.SetColumn(txtPrice, 2);
                dgDataTable.Children.Add(txtPrice);

                TextBlock txtAmt = new TextBlock();
                txtAmt.Text = String.Format("{0:0.000}", orderItem.Amt);
                txtAmt.FontSize = 11;
                txtAmt.VerticalAlignment = VerticalAlignment.Stretch;
                txtAmt.TextAlignment = TextAlignment.Right;
                txtAmt.Margin = new Thickness(0, 0, 0, 5);
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
        private void Generate_HeadText(BlockUIContainer blkHeadText, Owner owner)
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
            bimg.UriSource = new Uri(startupProjectPath + "\\Images\\" + owner.ImgName, UriKind.Absolute);
            bimg.EndInit();
            imgOwner.Source = bimg;
            imgOwner.HorizontalAlignment = HorizontalAlignment.Center;
            imgOwner.Margin = new Thickness(85, 0, 0, 0);
            stpLogo.Children.Add(imgOwner);



            string address = "";
            // modify the long address
            if (owner.Address.Length > 54)
            {
                string address1st = owner.Address.Substring(0, 53);
                string address2nd = owner.Address.Substring(53);
                address = address1st + "\n\t" + address2nd;
            }
            else
            {
                address = owner.Address;
            }



            TextBlock txtAddress = new TextBlock()
            {
                Text = "ADDRESS:  " + address,
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 10,
                FontWeight = FontWeights.UltraBold,

            };
            TextBlock txtPhone = new TextBlock()
            {
                Text = "PHONE:  " + owner.Phone,
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 10,
                Margin = new Thickness(0, 0, 0, 5),
                FontWeight = FontWeights.UltraBold,
            };
            StackPanel stpPageName = new StackPanel();
            TextBlock txtPageName = new TextBlock()
            {
                Text = owner.PageName,
                FontSize = 13,
                FontFamily = new FontFamily("Century Gothic"),
                FontWeight = FontWeights.UltraBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            stpPageName.Children.Add(txtPageName);


            stpHeadText.Children.Add(stpLogo);
            //stpHeadText.Children.Add(txtOwnerName);
            stpHeadText.Children.Add(txtAddress);
            stpHeadText.Children.Add(txtPhone);
            stpHeadText.Children.Add(stpPageName);

            blkHeadText.Child = stpHeadText;
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

    public class Owner
    {
        public string ImgName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PageName { get; set; }
    }
}
