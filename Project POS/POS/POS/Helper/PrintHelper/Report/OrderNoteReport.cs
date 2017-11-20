﻿using System;
using System.Collections.Generic;
using System.Linq;
using PdfRpt.Core.Contracts;
using PdfRpt.FluentInterface;
using POS.Entities.CustomEntities;
using POS.Helper.PrintHelper.Model;
using POS.Repository.DAL;

namespace POS.Helper.PrintHelper.Report
{
    public class OrderNoteReport : IListPdfReport
    {
        public IPdfReportData CreatePdfReport(AdminwsOfAsowell unitofwork, DateTime time, string folderName)
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
                    defaultHeader.Message("ORDER REPORT");
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

                var orderList = unitofwork.OrderRepository.Get().ToList();

                var orderWithTimeList = orderList.Where(x => x.Ordertime.Date.Equals(time.Date)).ToList();
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
                        payMethod = ((PaymentMethod) order.paymentMethod).ToString(),
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

        public IPdfReportData CreateDetailsPdfReport(AdminwsOfAsowell unitofwork, DateTime time, string folderName)
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
                    defaultHeader.Message("ORDER DETAILS REPORT");
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
                //var listOfRows = new List<User>
                //{
                //    new User {Id = 0, LastName = "Test Degree Sign: 120°", Name = "Celsius", Balance = 0}
                //};

                //for (var i = 1; i <= 200; i++)
                //{
                //    listOfRows.Add(new User { Id = i, LastName = "LastName " + i, Name = "Name " + i, Balance = i + 1000 });
                //}
                //dataSource.StronglyTypedList(listOfRows);

                var orderDetailsList = unitofwork.OrderNoteDetailsRepository.Get().ToList();

                var orderDetailsWithTimeList = orderDetailsList.Where(x => x.OrderNote.Ordertime.Date.Equals(time.Date)).ToList();
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
    }
}
