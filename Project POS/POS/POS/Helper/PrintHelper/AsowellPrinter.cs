using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;

namespace POS.Helper.PrintHelper
{
    public class AsowellPrinter
    {
        public void DoPrinting()
        {
            // Create Print Helper
            IPrintHelper printHelper = new ReceiptPrintHelper();

            // Create a FlowDocument dynamically.
            FlowDocument doc = printHelper.CreateDocument();
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

    }
}
