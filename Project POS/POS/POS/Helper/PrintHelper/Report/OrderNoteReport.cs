using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.Formula.Functions;
using PdfRpt.Core.Contracts;
using PdfRpt.FluentInterface;
using POS.Entities.CustomEntities;
using POS.Helper.PrintHelper.Model;
using POS.Repository.DAL;
using AggregateFunction = PdfRpt.Core.Contracts.AggregateFunction;

namespace POS.Helper.PrintHelper.Report
{
    /// <summary>
    /// Create Report about Order(income) in specific time
    /// </summary>
    public class OrderNoteReport : IListPdfReport
    {
        public IPdfReportData CreatePdfReport(AdminwsOfCloudPOS unitofwork, DateTime startTime, DateTime endTime, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Order Report" });
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
                    defaultHeader.Message("ORDER REPORT (" + startTime.ToShortDateString() + " - " + endTime.ToShortDateString() + ")");
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
                    endTime = endTime.AddDays(1);
                    var orderWithTimeList = unitofwork.OrderRepository.Get(x =>
                        x.Ordertime.CompareTo(startTime) >= 0 && x.Ordertime.CompareTo(endTime) <= 0);


                    List<OrderNoteForReport> orderReportList = new List<OrderNoteForReport>();
                    foreach (var order in orderWithTimeList)
                    {
                        var orderRpt = new OrderNoteForReport()
                        {
                            OrdernoteId = order.OrdernoteId,
                            CusId = order.CusId,
                            EmpId = order.EmpId,
                            Ordertable = order.Ordertable,
                            Ordertime = order.Ordertime,
                            TotalPrice = order.TotalPrice,
                            CustomerPay = order.CustomerPay,
                            PayBack = order.PayBack,
                            payMethod = ((PaymentMethod)order.paymentMethod).ToString(),
                        };
                        orderReportList.Add(orderRpt);
                    }

                    dataSource.StronglyTypedList(orderReportList);
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
                    column.PropertyName<OrderNoteForReport>(x => x.OrdernoteId);
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
                    column.PropertyName<OrderNoteForReport>(x => x.CusId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(2);
                    column.HeaderCell("Customer");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Emp");
                    column.PaddingLeft(25);
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.Ordertable);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Table");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.Ordertime);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Time");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.TotalPrice);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(6);
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
                    column.PropertyName<OrderNoteForReport>(x => x.CustomerPay);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(2);
                    column.HeaderCell("Customer Pay (kVND)");
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
                    column.PropertyName<OrderNoteForReport>(x => x.PayBack);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(8);
                    column.Width(2);
                    column.HeaderCell("Change (kVND)");
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
                    column.PropertyName<OrderNoteForReport>(x => x.payMethod);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(2);
                    column.HeaderCell("Payment");
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
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Order-Report-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }


        public IPdfReportData CreateDetailsPdfReport(AdminwsOfCloudPOS unitofwork, DateTime startTime, DateTime endTime, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Order Detail Report" });
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
                    defaultHeader.Message("ORDER DETAILS REPORT (" + startTime.ToShortDateString() + " - " + endTime.ToShortDateString() + ")");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue2Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(20);
            })
            .MainTableDataSource(dataSource =>
            {
                endTime = endTime.AddDays(1);
                var orderDetailsWithTimeList = unitofwork.OrderNoteDetailsRepository.Get(x =>
                    x.OrderNote.Ordertime.CompareTo(startTime) >= 0 && x.OrderNote.Ordertime.CompareTo(endTime) <= 0);



                List<OrderNoteDetailsForReport> orderDetailsReportList = new List<OrderNoteDetailsForReport>();
                foreach (var details in orderDetailsWithTimeList)
                {
                    var detailsRpt = new OrderNoteDetailsForReport()
                    {
                        OrdernoteId = details.OrdernoteId,
                        ProductId = details.ProductId,
                        ProductName = details.Product.Name,
                        EmpId = details.OrderNote.EmpId,
                        TableNumber = details.OrderNote.Ordertable,
                        OrderTime = details.OrderNote.Ordertime,
                        Quan = details.Quan,
                        PayMethod = ((PaymentMethod)details.OrderNote.paymentMethod).ToString()
                    };

                    orderDetailsReportList.Add(detailsRpt);
                }

                dataSource.StronglyTypedList(orderDetailsReportList);
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
                    column.PropertyName<OrderNoteDetailsForReport>(x => x.OrdernoteId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(3);
                    column.HeaderCell("Order ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Blue);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteDetailsForReport>(x => x.ProductId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Prod ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Crimson);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteDetailsForReport>(x => x.ProductName);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Prod Name");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteDetailsForReport>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(3);
                    column.HeaderCell("Emp ID");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteDetailsForReport>(x => x.TableNumber);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Table");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteDetailsForReport>(x => x.OrderTime);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(6);
                    column.Width(2);
                    column.HeaderCell("Time");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteDetailsForReport>(x => x.Quan);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(2);
                    column.HeaderCell("Qty");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteDetailsForReport>(x => x.PayMethod);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(2);
                    column.HeaderCell("Payment");
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
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Order-DetailsReport-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }


        public IPdfReportData CreateEntityPdfReport(AdminwsOfCloudPOS unitofwork, DateTime startTime, DateTime endTime, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Order Entities Report" });
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
                    defaultHeader.Message("ORDER ENTITIES REPORT (" + startTime.ToShortDateString() + " - " + endTime.ToShortDateString() + ")");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue2Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                //table.NumberOfDataRowsPerPage(20);
            })
            .MainTableDataSource(dataSource =>
            {
                endTime = endTime.AddDays(1);
                var orderDetailsWithTimeList = unitofwork.OrderNoteDetailsRepository.Get(x =>
                    x.OrderNote.Ordertime.CompareTo(startTime) >= 0 && x.OrderNote.Ordertime.CompareTo(endTime) <= 0);



                List<OrderEntityForReport> orderEntityReportList = new List<OrderEntityForReport>();
                foreach (var product in unitofwork.ProductRepository.Get().ToList())
                {
                    var queryWithProduct = orderDetailsWithTimeList.Where(x => x.ProductId.Equals(product.ProductId));

                    int billCount = queryWithProduct.Count();
                    int quan = 0;
                    decimal totalAmount = 0;
                    foreach (var orderDetails in queryWithProduct)
                    {
                        quan += orderDetails.Quan;
                        totalAmount += orderDetails.Quan * product.Price;
                    }

                    var orderEntity = new OrderEntityForReport()
                    {
                        Id = product.ProductId,
                        Name = product.Name,
                        Quan = quan,
                        BillCount = billCount,
                        TotalAmount = totalAmount
                    };

                    orderEntityReportList.Add(orderEntity);
                }

                dataSource.StronglyTypedList(orderEntityReportList);
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
                    column.PropertyName<OrderEntityForReport>(x => x.Id);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(3);
                    column.HeaderCell("ID", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Blue);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderEntityForReport>(x => x.Name);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Name", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Crimson);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderEntityForReport>(x => x.Quan);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Qty");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderEntityForReport>(x => x.BillCount);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(3);
                    column.HeaderCell("Bill Count");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderEntityForReport>(x => x.TotalAmount);
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
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Order-EntityReport-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }


        public IPdfReportData CreateMonthPdfReport(AdminwsOfCloudPOS unitofwork, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Order Report" });
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
                    defaultHeader.Message("ORDER REPORT (" + DateTime.Now.Month + "/" + DateTime.Now.Year + ")");
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
                var orderWithTimeList = unitofwork.OrderRepository.Get(x =>
                    x.Ordertime.Month == DateTime.Now.Month && x.Ordertime.Year == DateTime.Now.Year);


                List<OrderNoteForReport> orderReportList = new List<OrderNoteForReport>();
                foreach (var order in orderWithTimeList)
                {
                    var orderRpt = new OrderNoteForReport()
                    {
                        OrdernoteId = order.OrdernoteId,
                        CusId = order.CusId,
                        EmpId = order.EmpId,
                        Ordertable = order.Ordertable,
                        Ordertime = order.Ordertime,
                        TotalPrice = order.TotalPrice,
                        CustomerPay = order.CustomerPay,
                        PayBack = order.PayBack,
                        payMethod = ((PaymentMethod)order.paymentMethod).ToString(),
                    };
                    orderReportList.Add(orderRpt);
                }

                dataSource.StronglyTypedList(orderReportList);
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
                    column.PropertyName<OrderNoteForReport>(x => x.OrdernoteId);
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
                    column.PropertyName<OrderNoteForReport>(x => x.CusId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(2);
                    column.HeaderCell("Customer");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Emp");
                    column.PaddingLeft(25);
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.Ordertable);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Table");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.Ordertime);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Time");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.TotalPrice);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(6);
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
                    column.PropertyName<OrderNoteForReport>(x => x.CustomerPay);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(2);
                    column.HeaderCell("Customer Pay (kVND)");
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
                    column.PropertyName<OrderNoteForReport>(x => x.PayBack);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(8);
                    column.Width(2);
                    column.HeaderCell("Change (kVND)");
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
                    column.PropertyName<OrderNoteForReport>(x => x.payMethod);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(2);
                    column.HeaderCell("Payment");
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
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Order-Report-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }


        public IPdfReportData CreateDayPdfReport(AdminwsOfCloudPOS unitofwork, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Order Report" });
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
                    defaultHeader.Message("ORDER REPORT (" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + ")");
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
                var orderWithTimeList = unitofwork.OrderRepository.Get(x => x.Ordertime.Day == DateTime.Now.Day &&
                    x.Ordertime.Month == DateTime.Now.Month && x.Ordertime.Year == DateTime.Now.Year);


                List<OrderNoteForReport> orderReportList = new List<OrderNoteForReport>();
                foreach (var order in orderWithTimeList)
                {
                    var orderRpt = new OrderNoteForReport()
                    {
                        OrdernoteId = order.OrdernoteId,
                        CusId = order.CusId,
                        EmpId = order.EmpId,
                        Ordertable = order.Ordertable,
                        Ordertime = order.Ordertime,
                        TotalPrice = order.TotalPrice,
                        CustomerPay = order.CustomerPay,
                        PayBack = order.PayBack,
                        payMethod = ((PaymentMethod)order.paymentMethod).ToString(),
                    };
                    orderReportList.Add(orderRpt);
                }

                dataSource.StronglyTypedList(orderReportList);
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
                    column.PropertyName<OrderNoteForReport>(x => x.OrdernoteId);
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
                    column.PropertyName<OrderNoteForReport>(x => x.CusId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(2);
                    column.HeaderCell("Customer");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Emp");
                    column.PaddingLeft(25);
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.Ordertable);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Table");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.Ordertime);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Time");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.TotalPrice);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(6);
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
                    column.PropertyName<OrderNoteForReport>(x => x.CustomerPay);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(2);
                    column.HeaderCell("Customer Pay (kVND)");
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
                    column.PropertyName<OrderNoteForReport>(x => x.PayBack);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(8);
                    column.Width(2);
                    column.HeaderCell("Change (kVND)");
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
                    column.PropertyName<OrderNoteForReport>(x => x.payMethod);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(2);
                    column.HeaderCell("Payment");
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
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Order-Report-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }


        public IPdfReportData CreateYearPdfReport(AdminwsOfCloudPOS unitofwork, string folderName)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.LeftToRight);
                doc.Orientation(PageOrientation.Landscape);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Asowell Restaurant", Application = "Asowell POS", Keywords = "IList Rpt.", Subject = "Report", Title = "Order Report" });
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
                    defaultHeader.Message("ORDER REPORT (" + DateTime.Now.Year + ")");
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
                var orderWithTimeList = unitofwork.OrderRepository.Get(x => x.Ordertime.Year == DateTime.Now.Year);


                List<OrderNoteForReport> orderReportList = new List<OrderNoteForReport>();
                foreach (var order in orderWithTimeList)
                {
                    var orderRpt = new OrderNoteForReport()
                    {
                        OrdernoteId = order.OrdernoteId,
                        CusId = order.CusId,
                        EmpId = order.EmpId,
                        Ordertable = order.Ordertable,
                        Ordertime = order.Ordertime,
                        TotalPrice = order.TotalPrice,
                        CustomerPay = order.CustomerPay,
                        PayBack = order.PayBack,
                        payMethod = ((PaymentMethod)order.paymentMethod).ToString(),
                    };
                    orderReportList.Add(orderRpt);
                }

                dataSource.StronglyTypedList(orderReportList);
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
                    column.PropertyName<OrderNoteForReport>(x => x.OrdernoteId);
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
                    column.PropertyName<OrderNoteForReport>(x => x.CusId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(2);
                    column.HeaderCell("Customer");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.EmpId);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Emp");
                    column.PaddingLeft(25);
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.Ordertable);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Table");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.Ordertime);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(5);
                    column.Width(2);
                    column.HeaderCell("Time");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<OrderNoteForReport>(x => x.TotalPrice);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(6);
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
                    column.PropertyName<OrderNoteForReport>(x => x.CustomerPay);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(7);
                    column.Width(2);
                    column.HeaderCell("Customer Pay (kVND)");
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
                    column.PropertyName<OrderNoteForReport>(x => x.PayBack);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Right);
                    column.IsVisible(true);
                    column.Order(8);
                    column.Width(2);
                    column.HeaderCell("Change (kVND)");
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
                    column.PropertyName<OrderNoteForReport>(x => x.payMethod);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(9);
                    column.Width(2);
                    column.HeaderCell("Payment");
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
            .Generate(data => data.AsPdfFile(string.Format("{0}\\Order-Report-{1}.pdf", folderName, Guid.NewGuid().ToString("N"))));
        }
    }
}
