using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace POS.Helper.PrintHelper
{
    public class ShiftPrintHelper : IPrintHelper
    {
        public FlowDocument CreateDocument()
        {
            return CreateShiftDocument();
        }

        public FlowDocument CreateShiftDocument()
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


            // Info Text
            BlockUIContainer blkInfoText = new BlockUIContainer();
            Generate_InfoText(blkInfoText);

            // Table Text
            BlockUIContainer blkTableText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_TableText(blkTableText);

            // Summary Text
            BlockUIContainer blkSummaryText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_SummaryText(blkSummaryText);


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

        private void Generate_SummaryText(BlockUIContainer blkSummaryText)
        {

        }

        private void Generate_TableText(BlockUIContainer blkTableText)
        {

        }

        private void Generate_InfoText(BlockUIContainer blkInfoText)
        {

        }

        private void Generate_HeadText(BlockUIContainer blkHeadText)
        {

        }
    }
}
