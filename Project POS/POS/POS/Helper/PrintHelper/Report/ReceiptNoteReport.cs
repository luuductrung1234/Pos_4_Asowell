using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfRpt.Core.Contracts;
using PdfRpt.FluentInterface;
using POS.Entities;
using POS.Entities.CustomEntities;
using POS.Repository.DAL;

namespace POS.Helper.PrintHelper.Report
{
    public class ReceiptNoteReport : IListPdfReport
    {
        public IPdfReportData CreatePdfReport(AdminwsOfAsowell unitofwork, DateTime time, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Portrait);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Input Storage Report" });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
                doc.PrintingPreferences(new PrintingPreferences
                {
                    ShowPrintDialogAutomatically = true
                });
            })
            .DefaultFonts(fonts =>
            {
                fonts.Path(System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\arial.ttf"),
                           System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\verdana.ttf"));
                fonts.Size(9);
                fonts.Color(System.Drawing.Color.Black);
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter(DateTime.Now.ToString("MM/dd/yyyy"));
            })
            .PagesHeader(header =>
            {
                header.CacheHeader(cache: true); // It's a default setting to improve the performance.
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
                    defaultHeader.ImagePath(System.IO.Path.Combine(AppPath.ApplicationPath, "Images\\logo.png"));
                    defaultHeader.Message("INPUT STORAGE REPORT");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue1Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(5);
            })
            .MainTableDataSource(dataSource =>
            {
                //var listOfRows = new List<User>
                //{
                //    new User {Id = 0, LastName = "Test Degree Sign: 120°", Name = "Celsius", Balance = 0}
                //};

                //for (var i = 1; i <= 200; i++)
                //{
                //    listOfRows.Add(new User { Id = i, LastName = "LastName " + i, Name = "Name " + i, Balance = i + 1000 });
                //}
                //dataSource.StronglyTypedList(listOfRows);

                var receiptList = unitofwork.ReceiptNoteRepository.Get().ToList();

                var receiptWithTimeList = receiptList.Where(x => x.Inday.Date.Equals(time.Date));

                dataSource.StronglyTypedList(receiptWithTimeList);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                summarySettings.OverallSummarySettings("Summary");
                summarySettings.PreviousPageSummarySettings("Previous Page Summary");
                summarySettings.PageSummarySettings("Page Summary");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("#");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNote>(x => x.RnId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(3);
                    column.HeaderCell("ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Brown);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNote>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(2);
                    column.HeaderCell("Emp");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNote>(x => x.Employee.Name);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(2);
                    column.HeaderCell("Emp Name");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNote>(x => x.Inday);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Time");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNote>(x => x.TotalAmount);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Total Amount (kVND)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });
                

            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "There is no data available to display.");
            })
            .Export(export =>
            {
                export.ToExcel();
                export.ToCsv();
                export.ToXml();
            })
            .Generate(data => data.AsPdfFile(string.Format("{0}\\InStorage-Report-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }

        public IPdfReportData CreateDetailsPdfReport(AdminwsOfAsowell unitofwork, DateTime time, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Input Storage Details Report" });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
                doc.PrintingPreferences(new PrintingPreferences
                {
                    ShowPrintDialogAutomatically = true
                });
            })
            .DefaultFonts(fonts =>
            {
                fonts.Path(System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\arial.ttf"),
                           System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\verdana.ttf"));
                fonts.Size(9);
                fonts.Color(System.Drawing.Color.Black);
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter(DateTime.Now.ToString("MM/dd/yyyy"));
            })
            .PagesHeader(header =>
            {
                header.CacheHeader(cache: true); // It's a default setting to improve the performance.
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.LeftToRight);
                    defaultHeader.ImagePath(System.IO.Path.Combine(AppPath.ApplicationPath, "Images\\logo.png"));
                    defaultHeader.Message("INPUT STORAGE DETAILS REPORT");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue2Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(5);
            })
            .MainTableDataSource(dataSource =>
            {
                //var listOfRows = new List<User>
                //{
                //    new User {Id = 0, LastName = "Test Degree Sign: 120°", Name = "Celsius", Balance = 0}
                //};

                //for (var i = 1; i <= 200; i++)
                //{
                //    listOfRows.Add(new User { Id = i, LastName = "LastName " + i, Name = "Name " + i, Balance = i + 1000 });
                //}
                //dataSource.StronglyTypedList(listOfRows);

                var receiptDetailsList = unitofwork.ReceiptNoteDsetailsRepository.Get().ToList();

                var receiptDetailsWithTimeList = receiptDetailsList.Where(x => x.ReceiptNote.Inday.Date.Equals(time.Date));

                dataSource.StronglyTypedList(receiptDetailsWithTimeList);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                summarySettings.OverallSummarySettings("Summary");
                summarySettings.PreviousPageSummarySettings("Previous Page Summary");
                summarySettings.PageSummarySettings("Page Summary");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("#");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNoteDetail>(x => x.RnId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(3);
                    column.HeaderCell("Receipt ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Blue);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNoteDetail>(x => x.IgdId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Igd ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Crimson);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNoteDetail>(x => x.Ingredient.Name);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Igd Name");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNoteDetail>(x => x.ReceiptNote.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(3);
                    column.HeaderCell("Emp ID");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNoteDetail>(x => x.ReceiptNote.Inday);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Time");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNoteDetail>(x => x.Quan);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(6);
                    column.Width(2);
                    column.HeaderCell("Qty");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNoteDetail>(x => x.ItemPrice);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(2);
                    column.HeaderCell("Item Price (kVND)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    //column.AggregateFunction(aggregateFunction =>
                    //{
                    //    aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                    //    aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                    //        ? string.Empty : string.Format("{0:n0}", obj));
                    //});
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNoteDetail>(x => x.ReceiptNote.TotalAmount);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(8);
                    column.Width(2);
                    column.HeaderCell("Total Amount (kVND)");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<ReceiptNoteDetail>(x => x.Note);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(4);
                    column.HeaderCell("Note");
                });

            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "There is no data available to display.");
            })
            .Export(export =>
            {
                export.ToExcel();
                export.ToCsv();
                export.ToXml();
            })
            .Generate(data => data.AsPdfFile(string.Format("{0}\\InStorage-DetailsReport-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }
    }

}
