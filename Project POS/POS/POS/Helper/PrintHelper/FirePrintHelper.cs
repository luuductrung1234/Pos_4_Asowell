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
using System.Windows.Shapes;
using POS.Helper.PrintHelper.Model;

namespace POS.Helper.PrintHelper
{
    class FirePrintHelper: IPrintHelper
    {
        private static string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        public int TableNumer { get; set; }
        public string Mess { get; set; }

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
            Generate_HeadText(blkHeadText);


            //// Add Paragraph to Section
            //sec.Blocks.Add(p1);
            sec.Blocks.Add(blkHeadText);

            // Add Section to FlowDocument
            doc.Blocks.Add(sec);


            return doc;
        }


        /// <summary>
        /// Create the Head Section in Kitchen Print
        /// </summary>
        /// <param name="blkHeadText"></param>
        /// <param name="order"></param>
        private void Generate_HeadText(BlockUIContainer blkHeadText)
        {
            // Main stackPanel in Head Text
            StackPanel stpHeadText = new StackPanel();
            
            
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
                Text = TableNumer.ToString(),
                FontFamily = new FontFamily("Century Gothic"),
                FontSize = 18,
                Width = 30,
                FontWeight = FontWeights.UltraBold,
            };
            stpTableNumber.Children.Add(tbTableNumberLabel);
            stpTableNumber.Children.Add(tbTableNumber);


            StackPanel stpMess = new StackPanel();
            TextBlock tbMess = new TextBlock()
            {
                Text = Mess,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 20,
                Margin = new Thickness(0, 10, 0, 0),
                FontFamily = new FontFamily("Century Gothic"),
                FontWeight = FontWeights.UltraBold,
            };
            stpMess.Children.Add(tbMess);

            stpHeadText.Children.Add(stpTableNumber);
            stpHeadText.Children.Add(stpMess);

            blkHeadText.Child = stpHeadText;
        }
        
    }
}
