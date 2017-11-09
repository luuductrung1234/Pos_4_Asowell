using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace POS.Helper
{
    class PrintHelper
    {
        private static string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        private void PrintSimpleTextButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a FlowDocument dynamically.
            //FlowDocument doc = CreateFlowDocument();
            //doc.Name = "FlowDoc";

            // Create a PrintDialog
            PrintDialog printDlg = new PrintDialog();

            // Read the FlowDoucument xaml file
            //Stream flowDocumentStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TestWPF.PrintWindow.xaml");
            FileStream fs = new FileStream(startupProjectPath + "\\FlowDocument1.xaml", FileMode.Open, FileAccess.Read);
            FlowDocument flowDocument = (FlowDocument)XamlReader.Load(fs);

            // Create IDocumentPaginatorSource from FlowDocument
            IDocumentPaginatorSource idpSource = flowDocument;

            // Call PrintDocument method to send document to printer
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
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

            // Create a Section
            Section sec = new Section();
            Section sec2 = new Section();

            // Create first Paragraph
            Paragraph p1 = new Paragraph();
            // Create and add a new Bold, Italic and Underline
            Bold bld = new Bold();

            bld.Inlines.Add(new Run("First Paragraph"));
            Italic italicBld = new Italic();
            italicBld.Inlines.Add(bld);
            Underline underlineItalicBld = new Underline();
            underlineItalicBld.Inlines.Add(italicBld);
            // Add Bold, Italic, Underline to Paragraph
            p1.Inlines.Add(underlineItalicBld);


            // Create second Paragraph
            Paragraph p2 = new Paragraph();
            p2.Inlines.Add(underlineItalicBld);

            // Create second Paragraph
            Paragraph p3 = new Paragraph();
            p2.Inlines.Add(underlineItalicBld);



            // Add Paragraph to Section 1
            sec.Blocks.Add(p1);
            sec.Blocks.Add(p2);
            sec.Blocks.Add(p3);

            // Add Paragraph to Section 2
            sec2.Blocks.Add(p1);

            // Add Section to FlowDocument
            doc.Blocks.Add(sec);
            doc.Blocks.Add(sec2);

            //doc.PageWidth = 100;


            return doc;
        }
    }
}
