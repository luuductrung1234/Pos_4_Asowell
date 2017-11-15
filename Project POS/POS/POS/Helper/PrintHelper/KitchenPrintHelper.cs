using POS.Helper.PrintHelper.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace POS.Helper.PrintHelper
{
    public class KitchenPrintHelper: IPrintHelper
    {
        private static string startupProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

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
            

            // Table Text
            BlockUIContainer blkTableText = new BlockUIContainer()
            {
                Margin = new Thickness(0, 10, 0, 0)
            };
            Generate_TableText(blkTableText, order.getMetaKitchenTable(), order.OrderDetails);
            


            //// Add Paragraph to Section
            //sec.Blocks.Add(p1);
            sec.Blocks.Add(blkHeadText);
            sec.Blocks.Add(blkTableText);

            // Add Section to FlowDocument
            doc.Blocks.Add(sec);


            return doc;
        }

        
        private void Generate_HeadText(BlockUIContainer blkHeadText)
        {

        }

        public void Generate_TableText(BlockUIContainer blkTableText, string[] gridMeta, List<OrderDetailsForPrint> listData)
        {

        }
    }
}
