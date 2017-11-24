using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using POS.Helper.PrintHelper.Model;
using POS.Repository.DAL;
using Table = POS.Entities.Table;

namespace POS.Helper.PrintHelper
{
    public class DoPrintHelper
    {
        public static readonly int Receipt_Printing = 1;
        public static readonly int Kitchen_Printing = 2;
        public static readonly int Bar_Printing = 3;
        public static readonly int Eod_Printing = 4;

        private readonly EmployeewsOfAsowell _unitofwork;
        private IPrintHelper ph;
        private int type;
        private readonly Entities.Table curTable;

        public DoPrintHelper(EmployeewsOfAsowell unitofwork, int printType, Entities.Table currentTable)
        {
            _unitofwork = unitofwork;
            type = printType;
            curTable = currentTable;
        }

        public void DoPrint()
        {
            if (curTable == null && type != Eod_Printing)
            {
                return;
            }

            try
            {
                // Create a PrintHelper
                CreatePrintHelper();

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
                printDlg.PrintDocument(idpSource.DocumentPaginator, "bill printing");

                // convert FlowDocument to FixedDocument
                //var paginator = idpSource.DocumentPaginator;
                //var package = Package.Open(new MemoryStream(), FileMode.Create, FileAccess.ReadWrite);
                //var packUri = new Uri("pack://temp.xps");
                //PackageStore.RemovePackage(packUri);
                //PackageStore.AddPackage(packUri, package);
                //var xps = new XpsDocument(package, CompressionOption.NotCompressed, packUri.ToString());
                //XpsDocument.CreateXpsDocumentWriter(xps).Write(paginator);
                //FixedDocument fdoc = xps.GetFixedDocumentSequence().References[0].GetDocument(true);

                //DocumentViewer previewWindow = new DocumentViewer
                //{
                //    Document = fdoc
                //};
                //Window printpriview = new Window();
                //printpriview.Content = previewWindow;
                //printpriview.Title = "Print Preview";
                //printpriview.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CreatePrintHelper()
        {
            // Create Print Helper
            if (type == Receipt_Printing)
            {
                ph = new ReceiptPrintHelper()
                {
                    Owner = new Owner()
                    {
                        ImgName = "logo.png",
                        Address = "Address: f.7th, Fafilm Building, 6 St.Thai Van Lung, w.Ben Nghe, HCM City, Viet Nam",
                        Phone = "0927333668",
                        PageName = "RECEIPT"
                    },

                    Order = new OrderForPrint().GetAndConvertOrder(curTable, _unitofwork).GetAndConverOrderDetails(curTable, _unitofwork)
                };
            }

            if (type == Bar_Printing)
            {
                ph = new BarPrintHelper()
                {
                    Order = new OrderForPrint().GetAndConvertOrder(curTable, _unitofwork).GetAndConverOrderDetails(curTable, _unitofwork)
                };
            }

            if (type == Kitchen_Printing)
            {
                ph = new KitchenPrintHelper()
                {
                    Order = new OrderForPrint().GetAndConvertOrder(curTable, _unitofwork).GetAndConverOrderDetails(curTable, _unitofwork)
                };
            }

            if (type == Eod_Printing)
            {
                ph = new EndOfDayPrintHelper(_unitofwork);
            }
        }
    }
}
