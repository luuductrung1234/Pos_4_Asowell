using POS.Helper.PrintHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using POS.Entities;
using POS.Helper.PrintHelper.Model;


namespace POS.WPFTest
{
    /// <summary>
    /// Interaction logic for TestPrintWindow.xaml
    /// </summary>
    public partial class TestPrintWindow : Window
    {
        public TestPrintWindow()
        {
            InitializeComponent();
        }

        private void PrintSimpleTextButton_Click(object sender, RoutedEventArgs e)
        {
            // Create Print Helper
            IPrintHelper ph = new ReceiptPrintHelper()
            {
                Owner = new Owner()
                {
                    ImgName = "logo.png",
                    Address = "Address: f.7th, Fafilm Building, 6 St.Thai Van Lung, w.Ben Nghe, HCM City, Viet Nam",
                    Phone = "0927333668",
                    PageName = "RECEIPT"
                },

                Order = new OrderForPrint()
                {
                    No = "ORD0000001",
                    Table = 1,
                    Date = DateTime.Now,
                    Casher = "Luong Nhat Duy",
                    Customer = "Luu Duc Trung",
                    CustomerPay = 500,
                    OrderDetails = new List<OrderDetailsForPrint>()
                    {
                        new OrderDetailsForPrint(){Quan=2,ProductName="Pepsi",ProductPrice=25,Amt=50, SelectedStats = "Starter", ChairNumber = 1, Note = ""},
                        new OrderDetailsForPrint(){Quan=1,ProductName="Pepsi",ProductPrice=25,Amt=50, SelectedStats = "Dessert", ChairNumber = 2, Note = ""},
                        new OrderDetailsForPrint(){Quan=3,ProductName="Pepsi",ProductPrice=25,Amt=50, SelectedStats = "Starter", ChairNumber = 2, Note = "Nhieu da"},

                        new OrderDetailsForPrint(){Quan=1,ProductName="Coca Cola",ProductPrice=25,Amt=25, SelectedStats = "Starter", ChairNumber = 3, Note = "It da"},
                        new OrderDetailsForPrint(){Quan=1,ProductName="Coca Cola",ProductPrice=25,Amt=25, SelectedStats = "Starter", ChairNumber = 3, Note = ""},
                        new OrderDetailsForPrint(){Quan=2,ProductName="Coca Cola",ProductPrice=25,Amt=25, SelectedStats = "Starter", ChairNumber = 4, Note = ""},

                        new OrderDetailsForPrint(){Quan=1,ProductName="French Fries",ProductPrice=35,Amt=35, SelectedStats = "Starter", ChairNumber = 5, Note = "Chien it dau"},

                        new OrderDetailsForPrint(){Quan=1,ProductName="Honey Butter Bread",ProductPrice=70,Amt=170, SelectedStats = "MainCost", ChairNumber = 1, Note = ""},
                        new OrderDetailsForPrint(){Quan=1,ProductName="Honey Butter Bread",ProductPrice=70,Amt=170, SelectedStats = "Starter", ChairNumber = 3, Note = ""},
                        new OrderDetailsForPrint(){Quan=1,ProductName="Honey Butter Bread",ProductPrice=70,Amt=170, SelectedStats = "Starter", ChairNumber = 2, Note = ""},

                        new OrderDetailsForPrint(){Quan=1,ProductName="Comma Pizza",ProductPrice=50,Amt=150},
                        new OrderDetailsForPrint(){Quan=1,ProductName="Soda",ProductPrice=25,Amt=25},
                    }
                }
            };

            // Create a FlowDocument dynamically.
            FlowDocument doc = ph.CreateDocument();
            doc.Name = "FlowDoc";

            // Create a PrintDialog
            PrintDialog printDlg = new PrintDialog();
            // Setting the printer
            //printDlg.PrintQueue =new PrintQueue(new PrintServer(), "the printer name");

            // Read the FlowDoucument xaml file
            //Stream flowDocumentStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TestWPF.PrintWindow.xaml");
            //FileStream fs = new FileStream(startupProjectPath + "\\FlowDocument1.xaml", FileMode.Open, FileAccess.Read);
            //FlowDocument flowDocument = (FlowDocument)XamlReader.Load(fs);

            // Create IDocumentPaginatorSource from FlowDocument
            IDocumentPaginatorSource idpSource = doc;

            // Call PrintDocument method to send document to printer
            //printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");

            // convert FlowDocument to FixedDocument
            var paginator = idpSource.DocumentPaginator;
            var package = Package.Open(new MemoryStream(), FileMode.Create, FileAccess.ReadWrite);
            var packUri = new Uri("pack://temp.xps");
            PackageStore.RemovePackage(packUri);
            PackageStore.AddPackage(packUri, package);
            var xps = new XpsDocument(package, CompressionOption.NotCompressed, packUri.ToString());
            XpsDocument.CreateXpsDocumentWriter(xps).Write(paginator);
            FixedDocument fdoc = xps.GetFixedDocumentSequence().References[0].GetDocument(true);

            DocumentViewer previewWindow = new DocumentViewer
            {
                Document = fdoc
            };

            Window printpriview = new Window();
            printpriview.Content = previewWindow;
            printpriview.Title = "Print Preview";
            printpriview.Show();
        }

        // <summary>
        /// This method creates a dynamic FlowDocument. You can add anything to this
        /// FlowDocument that you would like to send to the printer
        /// </summary>
        /// <returns></returns>
        private FlowDocument CreateFlowDocument()
        {
            // Create a FlowDocument
            FlowDocument doc = new FlowDocument();

            // Set Margin
            doc.PagePadding = new Thickness(0);
            //doc.PagePadding = new Thickness(5, 10, 5, 10);


            // Set PageHeight and PageWidth to "Auto".
            doc.PageHeight = Double.NaN;
            doc.PageWidth = 290;
            //// Specify minimum page sizes.
            //doc.MinPageWidth = 680.0;
            //doc.MinPageHeight = 480.0;
            ////Specify maximum page sizes.
            //doc.MaxPageWidth = 1024.0;
            //doc.MaxPageHeight = 768.0;

            // Create a Section
            Section sec = new Section();

            //// Create first Paragraph
            //Paragraph p1 = new Paragraph();
            //// Create and add a new Bold, Italic and Underline
            //Bold bld = new Bold();
            //bld.Inlines.Add(new Run("First Paragraph"));
            //Italic italicBld = new Italic();
            //italicBld.Inlines.Add(bld);
            //Underline underlineItalicBld = new Underline();
            //underlineItalicBld.Inlines.Add(italicBld);
            //// Add Bold, Italic, Underline to Paragraph
            //p1.Inlines.Add(underlineItalicBld);

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



            //// Add Paragraph to Section
            //sec.Blocks.Add(p1);

            // Add Section to FlowDocument
            doc.Blocks.Add(sec);

            //doc.PageWidth = 100;


            return doc;
        }
    }
}
